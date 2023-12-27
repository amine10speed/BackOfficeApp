using BackOfficeApp.Data;
using System.Windows.Controls;
using System.Windows;
using BackOfficeApp.Views;

namespace BackOfficeApp;
public partial class MainWindow : Window
{
    private string employeNom = string.Empty; // Propriété pour stocker le nom

    public MainWindow()
    {
        InitializeComponent();
    }

    private void LoginButton_Click(object sender, RoutedEventArgs e)
    {
        string username = UsernameTextBox.Text;
        string password = PasswordBox.Password;
        bool isAdmin = AdminRadioButton.IsChecked == true;

        if (EstValide(username, password, isAdmin))
        {
            if (isAdmin)
            {
                // Ouvrir l'interface du tableau de bord de l'admin
                var adminDashboard = new AdminDashboardWindow();
                adminDashboard.Show();
                this.Close(); // Ferme la fenêtre de connexion
            }
            else
            {
                // Ouvrir l'interface du tableau de bord de l'employé
                var employeDashboard = new EmployeDashboardWindow(); // Assurez-vous que cette fenêtre est créée
                employeDashboard.Show();
                this.Close(); // Ferme la fenêtre de connexion
            }
        }
        else
        {
            MessageBox.Show("Nom d'utilisateur ou mot de passe incorrect.");
        }
    }



    private bool EstValide(string username, string password, bool isAdmin)
    {
        try
        {
            using (var context = new BibliothequeContext())
            {
                if (isAdmin)
                {
                    var admin = context.Admins
                                        .FirstOrDefault(a => a.NomUtilisateur == username && a.MotDePasse == password);
                    return admin != null;
                }
                else
                {
                    var employe = context.Employes
                                         .FirstOrDefault(e => e.NomUtilisateur == username && e.MotDePasse == password);
                    if (employe != null)
                    {
                        employeNom = employe.Nom; // Stocker le nom de l'employé
                        return true;
                    }
                    return false;
                }
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Une erreur est survenue : {ex.Message}");
            return false;
        }
    }

}


