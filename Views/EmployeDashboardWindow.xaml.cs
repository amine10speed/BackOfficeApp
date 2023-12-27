using BackOfficeApp.Data;
using BackOfficeApp.Models;
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

    }


}
