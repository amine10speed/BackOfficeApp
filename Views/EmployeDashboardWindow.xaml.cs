using BackOfficeApp.Data;
using BackOfficeApp.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace BackOfficeApp.Views
{
    /// <summary>
    /// Logique d'interaction pour EmployeDashboardWindow.xaml
    /// </summary>
    public partial class EmployeDashboardWindow : Window
    {
        // Liste pour stocker tous les livres - cette liste devrait être remplie à partir de votre source de données
        private List<Livre> _tousLesLivres = new List<Livre>();
        


        public EmployeDashboardWindow()
        {
            InitializeComponent();
            ChargerTousLesLivres();
            ChargerTousLesEmprunts();
            ChargerReservations();


        }

        private void ChargerTousLesLivres()
        {
            using (var context = new BibliothequeContext())
            {
                // Cette ligne charge les livres depuis la base de données et les affecte au DataGrid
                BooksDataGrid.ItemsSource = context.Livres.ToList();
            }
        }

        private void SearchBookButton_Click(object sender, RoutedEventArgs e)
        {
            string isbn = SearchISBNTextBox.Text;
            string title = SearchTitleTextBox.Text;
            string author = SearchAuthorTextBox.Text;
            int.TryParse(SearchYearTextBox.Text, out int year);

            using (var context = new BibliothequeContext())
            {
                // Construire la requête en utilisant LINQ
                var query = context.Livres.AsQueryable();

                if (!string.IsNullOrWhiteSpace(isbn))
                {
                    query = query.Where(livre => livre.ISBN.Contains(isbn));
                }
                if (!string.IsNullOrWhiteSpace(title))
                {
                    query = query.Where(livre => livre.Titre.Contains(title));
                }
                if (!string.IsNullOrWhiteSpace(author))
                {
                    query = query.Where(livre => livre.Auteur.Contains(author));
                }
                if (year > 0)
                {
                    query = query.Where(livre => livre.AnneePublication == year);
                }

                // Exécuter la requête et mettre à jour le DataGrid
                BooksDataGrid.ItemsSource = query.ToList();
            }
        }


        // partie gestion emprunts

        private void ChargerTousLesEmprunts()
        {
            using (var context = new BibliothequeContext())
            {
                LoansDataGrid.ItemsSource = context.Emprunts.Include(e => e.Adherent).Include(e => e.Livre).ToList();
            }
        }

        private void SearchLoanButton_Click(object sender, RoutedEventArgs e)
        {
            string searchAdherentName = SearchAdherentNameTextBox.Text.ToLower();
            string searchBookTitle = SearchBookTitleTextBox.Text.ToLower();

            using (var context = new BibliothequeContext())
            {
                var query = context.Emprunts.AsQueryable();

                if (!string.IsNullOrWhiteSpace(searchAdherentName))
                {
                    query = query.Where(emprunt => emprunt.Adherent.Nom.ToLower().Contains(searchAdherentName));
                }

                if (!string.IsNullOrWhiteSpace(searchBookTitle))
                {
                    query = query.Where(emprunt => emprunt.Livre.Titre.ToLower().Contains(searchBookTitle));
                }

                var results = query.Include(e => e.Adherent)
                                   .Include(e => e.Livre)
                                   .ToList();
                LoansDataGrid.ItemsSource = results;
            }
        }



        private void RegisterLoanButton_Click(object sender, RoutedEventArgs e)
        {
            int.TryParse(AdherentIdTextBox.Text, out int adherentId);
            string isbn = BookISBNTextBox.Text;
            DateTime dateEmprunt = LoanDatePicker.SelectedDate.GetValueOrDefault(DateTime.Now);

            Emprunt nouvelEmprunt = new Emprunt
            {
                AdherentID = adherentId,
                ISBN = isbn,
                DateEmprunt = dateEmprunt,
                DateRetourPrevu = dateEmprunt.AddDays(14) // Fixé à 14 jours après la date d'emprunt
            };

            using (var context = new BibliothequeContext())
            {
                context.Emprunts.Add(nouvelEmprunt);
                context.SaveChanges();
            }

            ChargerTousLesEmprunts();
        }


        private void ModifyLoanButton_Click(object sender, RoutedEventArgs e)
        {
            if (LoansDataGrid.SelectedItem is Emprunt selectedLoan)
            {
                selectedLoan.DateRetourReel = ReturnDatePicker.SelectedDate;
                using (var context = new BibliothequeContext())
                {
                    context.Emprunts.Update(selectedLoan);
                    context.SaveChanges();
                }

                ChargerTousLesEmprunts();
            }
        }


        private void DeleteLoanButton_Click(object sender, RoutedEventArgs e)
        {
            if (LoansDataGrid.SelectedItem is Emprunt selectedLoan)
            {
                using (var context = new BibliothequeContext())
                {
                    context.Emprunts.Remove(selectedLoan);
                    context.SaveChanges();
                }

                ChargerTousLesEmprunts();
            }
        }






        // partie gestion de reservation


        private void RegisterReservationButton_Click(object sender, RoutedEventArgs e)
        {
            int.TryParse(AdherentIdReservationTextBox.Text, out int adherentId);
            string isbn = BookISBNReservationTextBox.Text;
            DateTime dateReservation = ReservationDatePicker.SelectedDate.GetValueOrDefault(DateTime.Now);

            DateTime? datePrevuRetrait = TrouverPremiereDateRetourNonReservee(isbn);

            Reservation newReservation = new Reservation
            {
                AdherentID = adherentId,
                ISBN = isbn,
                DateReservation = dateReservation,
                DatePrevuRetrait = datePrevuRetrait
            };

            try
            {
                using (var context = new BibliothequeContext())
                {
                    context.Reservations.Add(newReservation);
                    context.SaveChanges();
                }

                MessageBox.Show("La réservation a été enregistrée avec succès.");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Une erreur est survenue lors de l'enregistrement de la réservation : {ex.Message}");
            }
            ChargerReservations();
        }

        private DateTime? TrouverPremiereDateRetourNonReservee(string isbn)
        {
            using (var context = new BibliothequeContext())
            {
                // Obtenez toutes les dates de retour prévues pour les emprunts de ce livre
                var emprunts = context.Emprunts
                    .Where(e => e.ISBN == isbn && e.DateRetourPrevu != DateTime.MinValue)
                    .Select(e => e.DateRetourPrevu)
                    .ToList();

                // Obtenez toutes les dates de retrait prévues pour les réservations de ce livre
                var reservations = context.Reservations
                    .Where(r => r.ISBN == isbn && r.DatePrevuRetrait.HasValue)
                    .Select(r => r.DatePrevuRetrait.Value)
                    .ToList();

                // Combinez et triez toutes les dates
                var toutesLesDates = emprunts.Union(reservations).OrderBy(d => d);

                foreach (var date in toutesLesDates)
                {
                    if (!reservations.Contains(date))
                    {
                        return date;
                    }
                }

                // Si aucune date n'est trouvée, retournez null ou une date par défaut
                return null;
            }
        }



        // Define the method to search for reservations.
        private void SearchReservationButton_Click(object sender, RoutedEventArgs e)
        {
            string adherentName = SearchAdherentNameReservationTextBox.Text;
            string bookTitle = SearchBookTitleReservationTextBox.Text;

            using (var context = new BibliothequeContext())
            {
                var query = context.Reservations.AsQueryable();

                if (!string.IsNullOrWhiteSpace(adherentName))
                {
                    query = query.Where(r => r.Adherent.Nom.Contains(adherentName));
                }
                if (!string.IsNullOrWhiteSpace(bookTitle))
                {
                    query = query.Where(r => r.Livre.Titre.Contains(bookTitle));
                }

                var results = query.Include(r => r.Adherent).Include(r => r.Livre).ToList();
                ReservationsDataGrid.ItemsSource = results;
            }
        }

        // Define the method to modify a selected reservation.
        private void ModifyReservationButton_Click(object sender, RoutedEventArgs e)
        {
            if (ReservationsDataGrid.SelectedItem is Reservation selectedReservation)
            {
                selectedReservation.DatePrevuRetrait = RetraitDatePicker?.SelectedDate;

                try
                {
                    using (var context = new BibliothequeContext())
                    {
                        context.Reservations.Update(selectedReservation);
                        context.SaveChanges();
                    }

                    MessageBox.Show("La réservation a été modifiée avec succès.");
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Une erreur est survenue lors de la modification de la réservation : {ex.Message}");
                }
            }
            else
            {
                MessageBox.Show("Veuillez sélectionner une réservation à modifier.");
            }

            ChargerReservations();
        }

        // Define the method to cancel a selected reservation.
        private void CancelReservationButton_Click(object sender, RoutedEventArgs e)
        {
            if (ReservationsDataGrid.SelectedItem is Reservation selectedReservation)
            {
                var confirmation = MessageBox.Show("Êtes-vous sûr de vouloir annuler cette réservation ?", "Confirmation", MessageBoxButton.YesNo);
                if (confirmation == MessageBoxResult.Yes)
                {
                    try
                    {
                        using (var context = new BibliothequeContext())
                        {
                            context.Reservations.Remove(selectedReservation);
                            context.SaveChanges();
                        }

                        MessageBox.Show("La réservation a été annulée avec succès.");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Une erreur est survenue lors de l'annulation de la réservation : {ex.Message}");
                    }
                }
            }
            else
            {
                MessageBox.Show("Veuillez sélectionner une réservation à annuler.");
            }

            ChargerReservations();
        }



        private void ChargerReservations()
        {
            using (var context = new BibliothequeContext())
            {
                // Assurez-vous d'avoir des noms corrects pour les propriétés de navigation dans vos modèles
                var reservations = context.Reservations
                                          .Include(r => r.Adherent)
                                          .Include(r => r.Livre)
                                          .ToList();

                ReservationsDataGrid.ItemsSource = reservations;
            }
        }










    }


}
