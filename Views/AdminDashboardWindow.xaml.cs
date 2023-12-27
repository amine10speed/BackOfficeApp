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
using BackOfficeApp.Data;
using CsvHelper;
using System.Globalization;
using System.IO;
using Microsoft.Win32;
using CsvHelper.Configuration;

namespace BackOfficeApp.Views
{
    /// <summary>
    /// Logique d'interaction pour AdminDashboardWindow.xaml
    /// </summary>
    public partial class AdminDashboardWindow : Window
    {
        public AdminDashboardWindow()
        {
            InitializeComponent();
        }

        private TextBox idTextBox;
        private TextBox nomUtilisateurTextBox;
        private PasswordBox motDePasseBox;
        private TextBox nomTextBox;
        private TextBox prenomTextBox;
        private TextBox adresseTextBox;
        private TextBox emailTextBox;
        private StackPanel infoPanel;
        private Button deleteButton;
        private TextBox fonctionTextBox; // Ajouté pour la fonction de l'employé
        private TextBox contactTextBox;

        private TextBox isbnTextBox; // Pour l'ISBN du livre
        private TextBox titreTextBox; // Pour le titre du livre
        private TextBox auteurTextBox; // Pour l'auteur du livre
        private TextBox anneePubTextBox; // Pour l'année de publication du livre
        private TextBox quantiteTextBox; // Pour la quantité du livre

        // la partie du code suivante est pour la gestion d'adherant
        private void BtnAjouterAdherent_Click(object sender, RoutedEventArgs e)
        {
            FormContainer.Children.Clear();

            // Initialisation des contrôles de saisie
            FormContainer.Children.Add(new TextBlock { Text = "Nom d'utilisateur", Margin = new Thickness(5) });
            nomUtilisateurTextBox = new TextBox { Margin = new Thickness(5), Name = "NomUtilisateurTextBox" };
            FormContainer.Children.Add(nomUtilisateurTextBox);

            FormContainer.Children.Add(new TextBlock { Text = "Mot de passe", Margin = new Thickness(5) });
            motDePasseBox = new PasswordBox { Margin = new Thickness(5), Name = "MotDePasseBox" };
            FormContainer.Children.Add(motDePasseBox);

            FormContainer.Children.Add(new TextBlock { Text = "Nom", Margin = new Thickness(5) });
            nomTextBox = new TextBox { Margin = new Thickness(5), Name = "NomTextBox" };
            FormContainer.Children.Add(nomTextBox);

            FormContainer.Children.Add(new TextBlock { Text = "Prénom", Margin = new Thickness(5) });
            prenomTextBox = new TextBox { Margin = new Thickness(5), Name = "PrenomTextBox" };
            FormContainer.Children.Add(prenomTextBox);

            FormContainer.Children.Add(new TextBlock { Text = "Adresse", Margin = new Thickness(5) });
            adresseTextBox = new TextBox { Margin = new Thickness(5), Name = "AdresseTextBox" };
            FormContainer.Children.Add(adresseTextBox);

            FormContainer.Children.Add(new TextBlock { Text = "Email", Margin = new Thickness(5) });
            emailTextBox = new TextBox { Margin = new Thickness(5), Name = "EmailTextBox" };
            FormContainer.Children.Add(emailTextBox);

            // Bouton de soumission
            var submitButton = new Button { Content = "Enregistrer", Margin = new Thickness(5) };
            submitButton.Click += SubmitForm_Click;
            FormContainer.Children.Add(submitButton);

            var resetButton = new Button { Content = "Réinitialiser", Margin = new Thickness(5) };
            resetButton.Click += ResetForm_Click; // Ajoutez cette méthode dans votre code-behind
            FormContainer.Children.Add(resetButton);
        }

        private void SubmitForm_Click(object sender, RoutedEventArgs e)
        {
            var adherent = new Adherent
            {
                NomUtilisateur = nomUtilisateurTextBox.Text,
                MotDePasse = motDePasseBox.Password, // Assurez-vous de hacher le mot de passe pour la sécurité
                Nom = nomTextBox.Text,
                Prenom = prenomTextBox.Text,
                Adresse = adresseTextBox.Text,
                Email = emailTextBox.Text
            };

            using (var context = new BibliothequeContext())
            {
                context.Adherents.Add(adherent);
                context.SaveChanges();
            }

            MessageBox.Show("Adhérent enregistré avec succès !");

            // Réinitialiser les champs du formulaire
            nomUtilisateurTextBox.Text = "";
            motDePasseBox.Password = "";
            nomTextBox.Text = "";
            prenomTextBox.Text = "";
            adresseTextBox.Text = "";
            emailTextBox.Text = "";
        }


        private void ResetForm_Click(object sender, RoutedEventArgs e)
        {
            nomUtilisateurTextBox.Text = "";
            motDePasseBox.Password = "";
            nomTextBox.Text = "";
            prenomTextBox.Text = "";
            adresseTextBox.Text = "";
            emailTextBox.Text = "";
        }

        private void BtnModifierAdherent_Click(object sender, RoutedEventArgs e)
        {
            FormContainer.Children.Clear();

            // Champ pour l'ID de l'adhérent
            idTextBox = new TextBox { Margin = new Thickness(5), Name = "IdTextBox" };
            FormContainer.Children.Add(new TextBlock { Text = "ID de l'Adhérent", Margin = new Thickness(5) });
            FormContainer.Children.Add(idTextBox);

            // Bouton pour la recherche
            var searchButton = new Button { Content = "Rechercher", Margin = new Thickness(5) };
            searchButton.Click += SearchButton_Click; // Assurez-vous d'implémenter cette méthode
            FormContainer.Children.Add(searchButton);

            // Initialisation des contrôles de saisie pour la modification
            nomUtilisateurTextBox = new TextBox { Margin = new Thickness(5) };
            FormContainer.Children.Add(new TextBlock { Text = "Nom d'utilisateur", Margin = new Thickness(5) });
            FormContainer.Children.Add(nomUtilisateurTextBox);

            motDePasseBox = new PasswordBox { Margin = new Thickness(5) };
            FormContainer.Children.Add(new TextBlock { Text = "Mot de passe", Margin = new Thickness(5) });
            FormContainer.Children.Add(motDePasseBox);

            nomTextBox = new TextBox { Margin = new Thickness(5) };
            FormContainer.Children.Add(new TextBlock { Text = "Nom", Margin = new Thickness(5) });
            FormContainer.Children.Add(nomTextBox);

            prenomTextBox = new TextBox { Margin = new Thickness(5) };
            FormContainer.Children.Add(new TextBlock { Text = "Prénom", Margin = new Thickness(5) });
            FormContainer.Children.Add(prenomTextBox);

            adresseTextBox = new TextBox { Margin = new Thickness(5) };
            FormContainer.Children.Add(new TextBlock { Text = "Adresse", Margin = new Thickness(5) });
            FormContainer.Children.Add(adresseTextBox);

            emailTextBox = new TextBox { Margin = new Thickness(5) };
            FormContainer.Children.Add(new TextBlock { Text = "Email", Margin = new Thickness(5) });
            FormContainer.Children.Add(emailTextBox);

            // Bouton de mise à jour
            var updateButton = new Button { Content = "Mettre à Jour", Margin = new Thickness(5) };
            updateButton.Click += UpdateForm_Click; // Assurez-vous d'implémenter cette méthode
            FormContainer.Children.Add(updateButton);
        }


        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            if (int.TryParse(idTextBox.Text, out int id))
            {
                using (var context = new BibliothequeContext())
                {
                    var adherent = context.Adherents.FirstOrDefault(a => a.AdherentID == id);
                    if (adherent != null)
                    {
                        // Remplir les champs du formulaire
                        nomUtilisateurTextBox.Text = adherent.NomUtilisateur;
                        // Ne pas remplir le mot de passe
                       
                        nomTextBox.Text = adherent.Nom;
                        prenomTextBox.Text = adherent.Prenom;
                        adresseTextBox.Text = adherent.Adresse;
                        emailTextBox.Text = adherent.Email;
                    }
                    else
                    {
                        MessageBox.Show("Aucun adhérent trouvé avec cet ID.");
                    }
                }
            }
            else
            {
                MessageBox.Show("Veuillez entrer un ID valide.");
            }
        }



        private void UpdateForm_Click(object sender, RoutedEventArgs e)
        {
            if (int.TryParse(idTextBox.Text, out int id))
            {
                using (var context = new BibliothequeContext())
                {
                    var adherentAModifier = context.Adherents.FirstOrDefault(a => a.AdherentID == id);
                    if (adherentAModifier != null)
                    {
                        // Mettre à jour les propriétés de l'adhérent avec les nouvelles valeurs
                        adherentAModifier.NomUtilisateur = nomUtilisateurTextBox.Text;
                        // Assurez-vous de hacher le mot de passe si nécessaire
                        if (!string.IsNullOrWhiteSpace(motDePasseBox.Password))
                        {
                            // Assurez-vous de hacher le mot de passe si nécessaire
                            adherentAModifier.MotDePasse = motDePasseBox.Password;
                        }
                        adherentAModifier.Nom = nomTextBox.Text;
                        adherentAModifier.Prenom = prenomTextBox.Text;
                        adherentAModifier.Adresse = adresseTextBox.Text;
                        adherentAModifier.Email = emailTextBox.Text;

                        context.SaveChanges();
                        MessageBox.Show("Adhérent mis à jour avec succès !");

                        // Réinitialiser les champs du formulaire
                        idTextBox.Text = "";
                        nomUtilisateurTextBox.Text = "";
                        motDePasseBox.Password = "";
                        nomTextBox.Text = "";
                        prenomTextBox.Text = "";
                        adresseTextBox.Text = "";
                        emailTextBox.Text = "";
                    }
                    else
                    {
                        MessageBox.Show("Aucun adhérent trouvé avec cet ID.");
                    }
                }
            }
            else
            {
                MessageBox.Show("Veuillez entrer un ID valide.");
            }
        }

        private void BtnRetirerAdherent_Click(object sender, RoutedEventArgs e)
        {
            FormContainer.Children.Clear();

            // Champ pour l'ID de l'adhérent
            idTextBox = new TextBox { Margin = new Thickness(5) };
            FormContainer.Children.Add(new TextBlock { Text = "ID de l'Adhérent", Margin = new Thickness(5) });
            FormContainer.Children.Add(idTextBox);

            // Bouton pour la recherche
            var searchButton = new Button { Content = "Rechercher", Margin = new Thickness(5) };
            searchButton.Click += SearchRetirerButton_Click;
            FormContainer.Children.Add(searchButton);

            // Panneau pour afficher les informations de l'adhérent
            infoPanel = new StackPanel();
            FormContainer.Children.Add(infoPanel);

            // Bouton pour la suppression
            deleteButton = new Button { Content = "Supprimer", Margin = new Thickness(5), Visibility = Visibility.Collapsed };
            deleteButton.Click += DeleteButton_Click;
            // Pas besoin de définir le nom ici
            FormContainer.Children.Add(deleteButton);
        }


        private void SearchRetirerButton_Click(object sender, RoutedEventArgs e)
        {
            if (int.TryParse(idTextBox.Text, out int id))
            {
                using (var context = new BibliothequeContext())
                {
                    var adherent = context.Adherents.FirstOrDefault(a => a.AdherentID == id);
                    if (adherent != null)
                    {
                        // Réinitialiser infoPanel avant d'afficher les nouvelles informations
                        infoPanel.Children.Clear();

                        // Afficher les informations de l'adhérent
                        infoPanel.Children.Add(new TextBlock { Text = $"Nom: {adherent.Nom}" });
                        infoPanel.Children.Add(new TextBlock { Text = $"Prénom: {adherent.Prenom}" });
                        infoPanel.Children.Add(new TextBlock { Text = $"Adresse: {adherent.Adresse}" });
                        infoPanel.Children.Add(new TextBlock { Text = $"Email: {adherent.Email}" });

                        // Rendre le bouton de suppression visible                       
                    
                            deleteButton.Visibility = Visibility.Visible;
                        
                    }
                    else
                    {
                        MessageBox.Show("Aucun adhérent trouvé avec cet ID.");
                    }
                }
            }
            else
            {
                MessageBox.Show("Veuillez entrer un ID valide.");
            }
        }



        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            // Tentez de convertir le texte dans idTextBox en un entier
            if (int.TryParse(idTextBox.Text, out int id))
            {
                using (var context = new BibliothequeContext())
                {
                    var adherentASupprimer = context.Adherents.FirstOrDefault(a => a.AdherentID == id);
                    if (adherentASupprimer != null)
                    {
                        // Suppression de l'adhérent de la base de données
                        context.Adherents.Remove(adherentASupprimer);
                        context.SaveChanges();
                        MessageBox.Show("Adhérent supprimé avec succès.");

                        // Réinitialiser le formulaire et cacher le bouton de suppression
                        idTextBox.Text = "";
                        infoPanel.Children.Clear();
                        
                        
                            deleteButton.Visibility = Visibility.Collapsed;
                        
                    }
                    else
                    {
                        MessageBox.Show("Aucun adhérent trouvé avec cet ID.");
                    }
                }
            }
            else
            {
                MessageBox.Show("Veuillez entrer un ID valide.");
            }
        }

        private void BtnImporterAdherents_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "CSV Files (*.csv)|*.csv";
            if (openFileDialog.ShowDialog() == true)
            {
                using (var reader = new StreamReader(openFileDialog.FileName))
                {
                    // Configure CsvReader to use a semicolon as the delimiter
                    var csvConfig = new CsvConfiguration(CultureInfo.InvariantCulture)
                    {
                        Delimiter = ";",
                        HasHeaderRecord = true,
                        MissingFieldFound = null
                    };
                    using (var csv = new CsvReader(reader, csvConfig))
                    {
                        csv.Context.RegisterClassMap<AdherentMap>(); // Register the class map
                        var adherents = csv.GetRecords<Adherent>().ToList(); // Read the records

                        using (var context = new BibliothequeContext())
                        {
                            foreach (var adherent in adherents)
                            {
                                adherent.AdherentID = 0; // Set AdherentID to 0 since it's auto-generated by the database
                                context.Adherents.Add(adherent); // Add the adherent to the context
                            }
                            context.SaveChanges(); // Save the changes to the database
                        }
                    }
                }
            }
        }


        // La classe de mappage pour ignorer l'ID si présent
        public sealed class AdherentMap : ClassMap<Adherent>
        {
            public AdherentMap()
            {
                // Remplacez les valeurs de la méthode Name par les en-têtes réels de votre fichier CSV
                Map(m => m.NomUtilisateur).Name("Nom d'utilisateur", "NomUtilisateur", "User Name", "username"); // Assurez-vous d'inclure toutes les variations possibles
                Map(m => m.MotDePasse).Name("Mot de passe", "MotDePasse", "Password", "password");
                Map(m => m.Nom).Name("Nom", "nom");
                Map(m => m.Prenom).Name("Prénom", "Prenom", "First Name", "firstname");
                Map(m => m.Adresse).Name("Adresse", "adresse", "Address", "address");
                Map(m => m.Email).Name("Email", "email");
                Map(m => m.AdherentID).Ignore(); // Ignorer l'ID car il est auto-généré
            }
        }


        private void BtnExporterAdherents_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "CSV Files (*.csv)|*.csv";
            if (saveFileDialog.ShowDialog() == true)
            {
                using (var writer = new StreamWriter(saveFileDialog.FileName))
                {
                    var csvConfig = new CsvConfiguration(CultureInfo.InvariantCulture)
                    {
                        Delimiter = ";",
                        Encoding = Encoding.UTF8 // Optionally, you may specify the encoding if needed.
                    };
                    using (var csv = new CsvWriter(writer, csvConfig))
                    {
                        using (var context = new BibliothequeContext())
                        {
                            var adherents = context.Adherents.ToList();
                            csv.WriteRecords(adherents);
                        }
                    }
                }
            }
        }








        // la partie suivante du code est pour la gestion d'employees:

        private void BtnAjouterEmploye_Click(object sender, RoutedEventArgs e)
        {
            FormContainer1.Children.Clear();

            // Initialisation des contrôles de saisie pour un employé
            FormContainer1.Children.Add(new TextBlock { Text = "Nom d'utilisateur", Margin = new Thickness(5) });
            nomUtilisateurTextBox = new TextBox { Margin = new Thickness(5) };
            FormContainer1.Children.Add(nomUtilisateurTextBox);

            FormContainer1.Children.Add(new TextBlock { Text = "Mot de passe", Margin = new Thickness(5) });
            motDePasseBox = new PasswordBox { Margin = new Thickness(5) };
            FormContainer1.Children.Add(motDePasseBox);

            FormContainer1.Children.Add(new TextBlock { Text = "Nom", Margin = new Thickness(5) });
            nomTextBox = new TextBox { Margin = new Thickness(5) };
            FormContainer1.Children.Add(nomTextBox);

            FormContainer1.Children.Add(new TextBlock { Text = "Prénom", Margin = new Thickness(5) });
            prenomTextBox = new TextBox { Margin = new Thickness(5) };
            FormContainer1.Children.Add(prenomTextBox);

            FormContainer1.Children.Add(new TextBlock { Text = "Fonction", Margin = new Thickness(5) });
            fonctionTextBox = new TextBox { Margin = new Thickness(5) };
            FormContainer1.Children.Add(fonctionTextBox);

            FormContainer1.Children.Add(new TextBlock { Text = "Contact", Margin = new Thickness(5) });
            contactTextBox = new TextBox { Margin = new Thickness(5) };
            FormContainer1.Children.Add(contactTextBox);

            // Bouton de soumission pour l'employé
            var submitButton = new Button { Content = "Enregistrer Employé", Margin = new Thickness(5) };
            submitButton.Click += SubmitEmployeForm_Click; // Vous devrez implémenter cette méthode
            FormContainer1.Children.Add(submitButton);

            // Bouton de réinitialisation pour l'employé
            var resetButton = new Button { Content = "Réinitialiser Formulaire", Margin = new Thickness(5) };
            resetButton.Click += ResetEmployeForm_Click; // Vous devrez implémenter cette méthode
            FormContainer1.Children.Add(resetButton);
        }

        private void SubmitEmployeForm_Click(object sender, RoutedEventArgs e)
        {
            var employe = new Employe
            {
                NomUtilisateur = nomUtilisateurTextBox.Text,
                MotDePasse = motDePasseBox.Password, // Assurez-vous de hacher le mot de passe pour la sécurité
                Nom = nomTextBox.Text,
                Prenom = prenomTextBox.Text,
                Fonction = fonctionTextBox.Text,
                Contact = contactTextBox.Text
            };

            using (var context = new BibliothequeContext())
            {
                context.Employes.Add(employe);
                context.SaveChanges();
            }

            MessageBox.Show("Employé enregistré avec succès !");

            // Réinitialiser les champs du formulaire
            nomUtilisateurTextBox.Text = "";
            motDePasseBox.Password = "";
            nomTextBox.Text = "";
            prenomTextBox.Text = "";
            fonctionTextBox.Text = "";
            contactTextBox.Text = "";
        }

        private void ResetEmployeForm_Click(object sender, RoutedEventArgs e)
        {
            nomUtilisateurTextBox.Text = "";
            motDePasseBox.Password = "";
            nomTextBox.Text = "";
            prenomTextBox.Text = "";
            fonctionTextBox.Text = "";  // Réinitialiser le champ de la fonction
            contactTextBox.Text = "";   // Réinitialiser le champ du contact
        }

        private void BtnModifierEmploye_Click(object sender, RoutedEventArgs e)
        {
            FormContainer1.Children.Clear();

            // Champ pour l'ID de l'employé
            idTextBox = new TextBox { Margin = new Thickness(5), Name = "IdTextBox" };
            FormContainer1.Children.Add(new TextBlock { Text = "ID de l'Employé", Margin = new Thickness(5) });
            FormContainer1.Children.Add(idTextBox);

            // Bouton pour la recherche
            var searchButton = new Button { Content = "Rechercher", Margin = new Thickness(5) };
            searchButton.Click += SearchEmployeButton_Click; // Implémentez cette méthode pour rechercher l'employé
            FormContainer1.Children.Add(searchButton);

            // Initialisation des contrôles de saisie pour la modification de l'employé
            nomUtilisateurTextBox = new TextBox { Margin = new Thickness(5) };
            FormContainer1.Children.Add(new TextBlock { Text = "Nom d'utilisateur", Margin = new Thickness(5) });
            FormContainer1.Children.Add(nomUtilisateurTextBox);

            motDePasseBox = new PasswordBox { Margin = new Thickness(5) };
            FormContainer1.Children.Add(new TextBlock { Text = "Mot de passe", Margin = new Thickness(5) });
            FormContainer1.Children.Add(motDePasseBox);

            nomTextBox = new TextBox { Margin = new Thickness(5) };
            FormContainer1.Children.Add(new TextBlock { Text = "Nom", Margin = new Thickness(5) });
            FormContainer1.Children.Add(nomTextBox);

            prenomTextBox = new TextBox { Margin = new Thickness(5) };
            FormContainer1.Children.Add(new TextBlock { Text = "Prénom", Margin = new Thickness(5) });
            FormContainer1.Children.Add(prenomTextBox);

            fonctionTextBox = new TextBox { Margin = new Thickness(5) };
            FormContainer1.Children.Add(new TextBlock { Text = "Fonction", Margin = new Thickness(5) });
            FormContainer1.Children.Add(fonctionTextBox);

            contactTextBox = new TextBox { Margin = new Thickness(5) };
            FormContainer1.Children.Add(new TextBlock { Text = "Contact", Margin = new Thickness(5) });
            FormContainer1.Children.Add(contactTextBox);

            // Bouton de mise à jour pour l'employé
            var updateButton = new Button { Content = "Mettre à Jour", Margin = new Thickness(5) };
            updateButton.Click += UpdateEmployeForm_Click; // Implémentez cette méthode pour mettre à jour l'employé
            FormContainer1.Children.Add(updateButton);
        }
        private void SearchEmployeButton_Click(object sender, RoutedEventArgs e)
        {
            if (int.TryParse(idTextBox.Text, out int id))
            {
                using (var context = new BibliothequeContext())
                {
                    var employe = context.Employes.FirstOrDefault(e => e.EmployeID == id);
                    if (employe != null)
                    {
                        // Remplir les champs du formulaire avec les informations de l'employé
                        nomUtilisateurTextBox.Text = employe.NomUtilisateur;
                        // Ne pas remplir le mot de passe car il ne devrait pas être affiché ou accessible en clair

                        nomTextBox.Text = employe.Nom;
                        prenomTextBox.Text = employe.Prenom;
                        fonctionTextBox.Text = employe.Fonction;
                        contactTextBox.Text = employe.Contact;
                    }
                    else
                    {
                        MessageBox.Show("Aucun employé trouvé avec cet ID.");
                    }
                }
            }
            else
            {
                MessageBox.Show("Veuillez entrer un ID valide.");
            }
        }


        private void UpdateEmployeForm_Click(object sender, RoutedEventArgs e)
        {
            if (int.TryParse(idTextBox.Text, out int id))
            {
                using (var context = new BibliothequeContext())
                {
                    var employeAModifier = context.Employes.FirstOrDefault(e => e.EmployeID == id);
                    if (employeAModifier != null)
                    {
                        // Mettre à jour les propriétés de l'employé avec les nouvelles valeurs
                        employeAModifier.NomUtilisateur = nomUtilisateurTextBox.Text;
                        // Ne mettez à jour le mot de passe que s'il a été modifié
                        if (!string.IsNullOrWhiteSpace(motDePasseBox.Password))
                        {
                            // Assurez-vous de hacher le mot de passe si nécessaire
                            employeAModifier.MotDePasse = motDePasseBox.Password;
                        }
                        employeAModifier.Nom = nomTextBox.Text;
                        employeAModifier.Prenom = prenomTextBox.Text;
                        employeAModifier.Fonction = fonctionTextBox.Text;
                        employeAModifier.Contact = contactTextBox.Text;

                        context.SaveChanges();
                        MessageBox.Show("Employé mis à jour avec succès !");

                        // Réinitialiser les champs du formulaire
                        idTextBox.Text = "";
                        nomUtilisateurTextBox.Text = "";
                        motDePasseBox.Password = "";
                        nomTextBox.Text = "";
                        prenomTextBox.Text = "";
                        fonctionTextBox.Text = "";
                        contactTextBox.Text = "";
                    }
                    else
                    {
                        MessageBox.Show("Aucun employé trouvé avec cet ID.");
                    }
                }
            }
            else
            {
                MessageBox.Show("Veuillez entrer un ID valide.");
            }
        }


        private void BtnSupprimerEmploye_Click(object sender, RoutedEventArgs e)
        {
            FormContainer1.Children.Clear();

            // Champ pour l'ID de l'employé
            idTextBox = new TextBox { Margin = new Thickness(5) };
            FormContainer1.Children.Add(new TextBlock { Text = "ID de l'Employé", Margin = new Thickness(5) });
            FormContainer1.Children.Add(idTextBox);

            // Bouton pour la recherche
            var searchButton = new Button { Content = "Rechercher", Margin = new Thickness(5) };
            searchButton.Click += SearchRetirerEmployeButton_Click; // Implémentez cette méthode pour rechercher l'employé
            FormContainer1.Children.Add(searchButton);

            // Panneau pour afficher les informations de l'employé
            infoPanel = new StackPanel();
            FormContainer1.Children.Add(infoPanel);

            // Bouton pour la suppression
            deleteButton = new Button { Content = "Supprimer Employé", Margin = new Thickness(5), Visibility = Visibility.Collapsed };
            deleteButton.Click += DeleteEmployeButton_Click; // Implémentez cette méthode pour supprimer l'employé
            FormContainer1.Children.Add(deleteButton);
        }

        private void SearchRetirerEmployeButton_Click(object sender, RoutedEventArgs e)
        {
            if (int.TryParse(idTextBox.Text, out int id))
            {
                using (var context = new BibliothequeContext())
                {
                    var employe = context.Employes.FirstOrDefault(e => e.EmployeID == id);
                    if (employe != null)
                    {
                        // Réinitialiser infoPanel avant d'afficher les nouvelles informations
                        infoPanel.Children.Clear();

                        // Afficher les informations de l'employé
                        infoPanel.Children.Add(new TextBlock { Text = $"Nom d'utilisateur: {employe.NomUtilisateur}" });
                        infoPanel.Children.Add(new TextBlock { Text = $"Nom: {employe.Nom}" });
                        infoPanel.Children.Add(new TextBlock { Text = $"Prénom: {employe.Prenom}" });
                        infoPanel.Children.Add(new TextBlock { Text = $"Fonction: {employe.Fonction}" });
                        infoPanel.Children.Add(new TextBlock { Text = $"Contact: {employe.Contact}" });

                        // Rendre le bouton de suppression visible
                        deleteButton.Visibility = Visibility.Visible;
                    }
                    else
                    {
                        MessageBox.Show("Aucun employé trouvé avec cet ID.");
                    }
                }
            }
            else
            {
                MessageBox.Show("Veuillez entrer un ID valide.");
            }
        }


        private void DeleteEmployeButton_Click(object sender, RoutedEventArgs e)
        {
            // Tentez de convertir le texte dans idTextBox en un entier
            if (int.TryParse(idTextBox.Text, out int id))
            {
                using (var context = new BibliothequeContext())
                {
                    var employeASupprimer = context.Employes.FirstOrDefault(e => e.EmployeID == id);
                    if (employeASupprimer != null)
                    {
                        // Suppression de l'employé de la base de données
                        context.Employes.Remove(employeASupprimer);
                        context.SaveChanges();
                        MessageBox.Show("Employé supprimé avec succès.");

                        // Réinitialiser le formulaire et cacher le bouton de suppression
                        idTextBox.Text = "";
                        infoPanel.Children.Clear();
                        deleteButton.Visibility = Visibility.Collapsed;
                    }
                    else
                    {
                        MessageBox.Show("Aucun employé trouvé avec cet ID.");
                    }
                }
            }
            else
            {
                MessageBox.Show("Veuillez entrer un ID valide.");
            }
        }

        // partie de gestion de livres:


        private void BtnAjouterLivre_Click(object sender, RoutedEventArgs e)
        {
            FormContainer2.Children.Clear();

            // Initialisation des contrôles de saisie pour un livre
            FormContainer2.Children.Add(new TextBlock { Text = "ISBN", Margin = new Thickness(5) });
            isbnTextBox = new TextBox { Margin = new Thickness(5) };
            FormContainer2.Children.Add(isbnTextBox);

            FormContainer2.Children.Add(new TextBlock { Text = "Titre", Margin = new Thickness(5) });
            titreTextBox = new TextBox { Margin = new Thickness(5) };
            FormContainer2.Children.Add(titreTextBox);

            FormContainer2.Children.Add(new TextBlock { Text = "Auteur", Margin = new Thickness(5) });
            auteurTextBox = new TextBox { Margin = new Thickness(5) };
            FormContainer2.Children.Add(auteurTextBox);

            FormContainer2.Children.Add(new TextBlock { Text = "Année de Publication", Margin = new Thickness(5) });
            anneePubTextBox = new TextBox { Margin = new Thickness(5) };
            FormContainer2.Children.Add(anneePubTextBox);

            FormContainer2.Children.Add(new TextBlock { Text = "Quantité", Margin = new Thickness(5) });
            quantiteTextBox = new TextBox { Margin = new Thickness(5) };
            FormContainer2.Children.Add(quantiteTextBox);

            // Bouton de soumission pour le livre
            var submitButton = new Button { Content = "Enregistrer Livre", Margin = new Thickness(5) };
            submitButton.Click += SubmitLivreForm_Click; // Implémentez cette méthode pour enregistrer le livre
            FormContainer2.Children.Add(submitButton);

            var resetButton = new Button { Content = "Réinitialiser Formulaire", Margin = new Thickness(5) };
            resetButton.Click += ResetLivreForm_Click; // Implémentez cette méthode pour réinitialiser le formulaire
            FormContainer2.Children.Add(resetButton);
        }


        private void SubmitLivreForm_Click(object sender, RoutedEventArgs e)
        {
            // Création d'un nouveau livre à partir des informations saisies
            var livre = new Livre
            {
                ISBN = isbnTextBox.Text,
                Titre = titreTextBox.Text,
                Auteur = auteurTextBox.Text,
                AnneePublication = int.Parse(anneePubTextBox.Text), // Assurez-vous que c'est un nombre valide
                Quantite = int.Parse(quantiteTextBox.Text)          // Assurez-vous que c'est un nombre valide
            };

            using (var context = new BibliothequeContext())
            {
                // Ajout du livre à la base de données
                context.Livres.Add(livre);
                context.SaveChanges();
            }

            MessageBox.Show("Livre enregistré avec succès !");

            // Réinitialiser les champs du formulaire
            isbnTextBox.Text = "";
            titreTextBox.Text = "";
            auteurTextBox.Text = "";
            anneePubTextBox.Text = "";
            quantiteTextBox.Text = "";
        }


        private void ResetLivreForm_Click(object sender, RoutedEventArgs e)
        {
            // Réinitialiser les champs du formulaire pour les livres
            isbnTextBox.Text = "";
            titreTextBox.Text = "";
            auteurTextBox.Text = "";
            anneePubTextBox.Text = "";
            quantiteTextBox.Text = "";
        }


        private void BtnModifierLivre_Click(object sender, RoutedEventArgs e)
        {
            FormContainer2.Children.Clear();

            // Champ pour l'ISBN du livre
            isbnTextBox = new TextBox { Margin = new Thickness(5) };
            FormContainer2.Children.Add(new TextBlock { Text = "ISBN du Livre", Margin = new Thickness(5) });
            FormContainer2.Children.Add(isbnTextBox);

            // Bouton pour la recherche
            var searchButton = new Button { Content = "Rechercher", Margin = new Thickness(5) };
            searchButton.Click += SearchLivreButton_Click; // Assurez-vous d'implémenter cette méthode
            FormContainer2.Children.Add(searchButton);

            // Initialisation des contrôles de saisie pour la modification du livre
            titreTextBox = new TextBox { Margin = new Thickness(5) };
            FormContainer2.Children.Add(new TextBlock { Text = "Titre", Margin = new Thickness(5) });
            FormContainer2.Children.Add(titreTextBox);

            auteurTextBox = new TextBox { Margin = new Thickness(5) };
            FormContainer2.Children.Add(auteurTextBox);

            anneePubTextBox = new TextBox { Margin = new Thickness(5) };
            FormContainer2.Children.Add(new TextBlock { Text = "Année de Publication", Margin = new Thickness(5) });
            FormContainer2.Children.Add(anneePubTextBox);

            quantiteTextBox = new TextBox { Margin = new Thickness(5) };
            FormContainer2.Children.Add(new TextBlock { Text = "Quantité", Margin = new Thickness(5) });
            FormContainer2.Children.Add(quantiteTextBox);

            // Bouton de mise à jour pour le livre
            var updateButton = new Button { Content = "Mettre à Jour Livre", Margin = new Thickness(5) };
            updateButton.Click += UpdateLivreForm_Click; // Implémentez cette méthode pour mettre à jour le livre
            FormContainer2.Children.Add(updateButton);
        }


        private void SearchLivreButton_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(isbnTextBox.Text))
            {
                using (var context = new BibliothequeContext())
                {
                    var livre = context.Livres.FirstOrDefault(l => l.ISBN == isbnTextBox.Text);
                    if (livre != null)
                    {
                        // Remplir les champs du formulaire avec les informations du livre
                        titreTextBox.Text = livre.Titre;
                        auteurTextBox.Text = livre.Auteur;
                        anneePubTextBox.Text = livre.AnneePublication.ToString();
                        quantiteTextBox.Text = livre.Quantite.ToString();
                    }
                    else
                    {
                        MessageBox.Show("Aucun livre trouvé avec cet ISBN.");
                    }
                }
            }
            else
            {
                MessageBox.Show("Veuillez entrer un ISBN valide.");
            }
        }

        private void UpdateLivreForm_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(isbnTextBox.Text))
            {
                using (var context = new BibliothequeContext())
                {
                    var livreAModifier = context.Livres.FirstOrDefault(l => l.ISBN == isbnTextBox.Text);
                    if (livreAModifier != null)
                    {
                        // Mettre à jour les propriétés du livre avec les nouvelles valeurs
                        livreAModifier.Titre = titreTextBox.Text;
                        livreAModifier.Auteur = auteurTextBox.Text;

                        if (int.TryParse(anneePubTextBox.Text, out int anneePublication))
                        {
                            livreAModifier.AnneePublication = anneePublication;
                        }
                        else
                        {
                            MessageBox.Show("Veuillez entrer une année de publication valide.");
                            return;
                        }

                        if (int.TryParse(quantiteTextBox.Text, out int quantite))
                        {
                            livreAModifier.Quantite = quantite;
                        }
                        else
                        {
                            MessageBox.Show("Veuillez entrer une quantité valide.");
                            return;
                        }

                        context.SaveChanges();
                        MessageBox.Show("Livre mis à jour avec succès !");

                        // Réinitialiser les champs du formulaire
                        isbnTextBox.Text = "";
                        titreTextBox.Text = "";
                        auteurTextBox.Text = "";
                        anneePubTextBox.Text = "";
                        quantiteTextBox.Text = "";
                    }
                    else
                    {
                        MessageBox.Show("Aucun livre trouvé avec cet ISBN.");
                    }
                }
            }
            else
            {
                MessageBox.Show("Veuillez entrer un ISBN valide.");
            }
        }


        private void BtnRetirerLivre_Click(object sender, RoutedEventArgs e)
        {
            FormContainer2.Children.Clear();

            // Champ pour l'ISBN du livre
            isbnTextBox = new TextBox { Margin = new Thickness(5) };
            FormContainer2.Children.Add(new TextBlock { Text = "ISBN du Livre", Margin = new Thickness(5) });
            FormContainer2.Children.Add(isbnTextBox);

            // Bouton pour la recherche
            var searchButton = new Button { Content = "Rechercher", Margin = new Thickness(5) };
            searchButton.Click += SearchRetirerLivreButton_Click; // Implémentez cette méthode pour rechercher le livre
            FormContainer2.Children.Add(searchButton);

            // Panneau pour afficher les informations du livre
            infoPanel = new StackPanel();
            FormContainer2.Children.Add(infoPanel);

            // Bouton pour la suppression
            deleteButton = new Button { Content = "Supprimer Livre", Margin = new Thickness(5), Visibility = Visibility.Collapsed };
            deleteButton.Click += DeleteLivreButton_Click; // Implémentez cette méthode pour supprimer le livre
            FormContainer2.Children.Add(deleteButton);
        }


        private void SearchRetirerLivreButton_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(isbnTextBox.Text))
            {
                using (var context = new BibliothequeContext())
                {
                    var livre = context.Livres.FirstOrDefault(l => l.ISBN == isbnTextBox.Text);
                    if (livre != null)
                    {
                        // Réinitialiser infoPanel avant d'afficher les nouvelles informations
                        infoPanel.Children.Clear();

                        // Afficher les informations du livre
                        infoPanel.Children.Add(new TextBlock { Text = $"ISBN: {livre.ISBN}" });
                        infoPanel.Children.Add(new TextBlock { Text = $"Titre: {livre.Titre}" });
                        infoPanel.Children.Add(new TextBlock { Text = $"Auteur: {livre.Auteur}" });
                        infoPanel.Children.Add(new TextBlock { Text = $"Année de Publication: {livre.AnneePublication}" });
                        infoPanel.Children.Add(new TextBlock { Text = $"Quantité: {livre.Quantite}" });

                        // Rendre le bouton de suppression visible
                        deleteButton.Visibility = Visibility.Visible;
                    }
                    else
                    {
                        MessageBox.Show("Aucun livre trouvé avec cet ISBN.");
                    }
                }
            }
            else
            {
                MessageBox.Show("Veuillez entrer un ISBN valide.");
            }
        }


        private void DeleteLivreButton_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(isbnTextBox.Text))
            {
                using (var context = new BibliothequeContext())
                {
                    var livreASupprimer = context.Livres.FirstOrDefault(l => l.ISBN == isbnTextBox.Text);
                    if (livreASupprimer != null)
                    {
                        // Suppression du livre de la base de données
                        context.Livres.Remove(livreASupprimer);
                        context.SaveChanges();
                        MessageBox.Show("Livre supprimé avec succès.");

                        // Réinitialiser le formulaire et cacher le bouton de suppression
                        isbnTextBox.Text = "";
                        infoPanel.Children.Clear();
                        deleteButton.Visibility = Visibility.Collapsed;
                    }
                    else
                    {
                        MessageBox.Show("Aucun livre trouvé avec cet ISBN.");
                    }
                }
            }
            else
            {
                MessageBox.Show("Veuillez entrer un ISBN valide.");
            }
        }





    }
}
