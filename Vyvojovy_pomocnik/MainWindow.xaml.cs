using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Vyvojovy_pomocnik.Classes;
using static Vyvojovy_pomocnik.MainWindow;
using Button = System.Windows.Controls.Button;
using Convert = Vyvojovy_pomocnik.Classes.Convertor;
using Cursors = System.Windows.Input.Cursors;

namespace Vyvojovy_pomocnik
{
    public partial class MainWindow : System.Windows.Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        #region Border
        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                this.DragMove();
            }
        }

        private bool IsMaximalized = false;

        private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ClickCount == 2)
            {
                if (IsMaximalized)
                {
                    this.WindowState = WindowState.Normal;
                    this.Width = 1080;
                    this.Height = 720;

                    Grid_detail_projekt2.Margin = new Thickness(0);
                    Pridat_projektGrid.Margin = new Thickness(0);
                    Pridat_ukolGrid.Margin = new Thickness(0);
                    Grid_detail_ukol2.Margin = new Thickness(0);
                    bug_projekt.Margin = new Thickness(0);
                    problem_projekt.Margin = new Thickness(0);
                    IsMaximalized = false;
                }
                else
                {
                    this.WindowState = WindowState.Maximized;

                    Grid_detail_projekt2.Margin = new Thickness(400, 0, 0, 0);
                    Pridat_projektGrid.Margin = new Thickness(400, 0, 0, 0);
                    Pridat_ukolGrid.Margin = new Thickness(400, 0, 0, 0);
                    Grid_detail_ukol2.Margin = new Thickness(400, 0, 0, 0);
                    bug_projekt.Margin = new Thickness(400, 0, 0, 0);
                    problem_projekt.Margin = new Thickness(400, 0, 0, 0);
                    IsMaximalized = true;
                }
            }
        }

        private void MaximalizedButton_Click(object sender, RoutedEventArgs e)
        {
            if (IsMaximalized)
            {
                this.WindowState = WindowState.Normal;
                this.Width = 1080;
                this.Height = 720;

                Grid_detail_projekt2.Margin = new Thickness(0);
                Pridat_projektGrid.Margin = new Thickness(0);
                Pridat_ukolGrid.Margin = new Thickness(0);
                Grid_detail_ukol2.Margin = new Thickness(0);
                bug_projekt.Margin = new Thickness(0);
                problem_projekt.Margin = new Thickness(0);
                IsMaximalized = false;
            }
            else
            {
                this.WindowState = WindowState.Maximized;

                Grid_detail_projekt2.Margin = new Thickness(400, 0, 0, 0);
                Pridat_projektGrid.Margin = new Thickness(400, 0, 0, 0);
                Pridat_ukolGrid.Margin = new Thickness(400, 0, 0, 0);
                Grid_detail_ukol2.Margin = new Thickness(400, 0, 0, 0);
                bug_projekt.Margin = new Thickness(400, 0, 0, 0);
                problem_projekt.Margin = new Thickness(400, 0, 0, 0);
                IsMaximalized = true;
            }
        }

        #endregion Border

        // Uzavření aplikace v horním pravém rohu
        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private int Aktivni_Polozka
        {
            get; set;
        }

        public int Aktivni_ID
        {
            get; set;
        }

        private bool Abecedne
        {
            get; set;
        }

        private void MenaVyber(object sender, RoutedEventArgs e)
        {
            VyberMena = Mena_nazev.Text;
        }

        private void ObnovitDB(object sender, RoutedEventArgs e)
        {
            try
            {
                String sql = "TRUNCATE TABLE Bugy";

                DB.QueryVoid(null, sql);
                sql = "TRUNCATE TABLE Problemy";
                DB.QueryVoid(null, sql);
                sql = "TRUNCATE TABLE dbo.Ukoly";
                DB.QueryVoid(null, sql);
                sql = "TRUNCATE TABLE dbo.Projekty";

                if (DB.QueryVoid(null, sql))
                {
                    Zprava_ukolu.Text = "Obnovena veškerá data v DB!";
                }
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(ex.Message);
                return;
            }
        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            if (Aktivni_Polozka == 1)
            {
                MenuItem1Button_Click(sender, e);
            }
            else if (Aktivni_Polozka == 2)
            {
                MenuItem2Button_Click(sender, e);
            }
        }

        private void SettingsButton_Click(object sender, RoutedEventArgs e)
        {
            MenuItemClear();
            tcSample.SelectedIndex = 8;
        }

        private void AlarmButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BugButton_Click(object sender, RoutedEventArgs e)
        {
            pole_idprojektu3.Items.Clear();

            foreach (var projekt in projekty)
            {
                ComboBoxItem item = new ComboBoxItem
                {
                    Content = projekt.Id + ". " + projekt.Nazev
                };
                pole_idprojektu3.Items.Add(item);
            }

            pole_id3.Items.Clear();

            MenuItem2Button_Click(sender, e);

            foreach (var ukol in ukoly)
            {
                ComboBoxItem item = new ComboBoxItem
                {
                    Content = ukol.Id + ". " + ukol.Nazev
                };
                pole_id3.Items.Add(item);
            }

            if (Counter.CountRow(1) > 0)
            {
                MenuItemClear();
                tcSample.SelectedIndex = 12;
            }
            else
            {
                Zprava_ukolu.Text = "Nejprve vytvořte nový projekt!";
            }
        }

        private void ErrorButton_Click(object sender, RoutedEventArgs e)
        {
            pole_idprojektu4.Items.Clear();

            foreach (var projekt in projekty)
            {
                ComboBoxItem item = new ComboBoxItem()
                {
                    Content = projekt.Id + ". " + projekt.Nazev,
                };
                pole_idprojektu4.Items.Add(item);
            }

            pole_id4.Items.Clear();

            MenuItem2Button_Click(sender, e);

            foreach (var ukol in ukoly)
            {
                ComboBoxItem item = new ComboBoxItem()
                {
                    Content = ukol.Id + ". " + ukol.Nazev,
                };
                pole_id4.Items.Add(item);
            }

            if (Counter.CountRow(1) > 0)
            {
                MenuItemClear();
                tcSample.SelectedIndex = 13;
            }
            else
            {
                Zprava_ukolu.Text = "Nejprve vytvořte nový projekt!";
            }
        }

        private void AddDBBug_Click(object sender, RoutedEventArgs e)
        {
            int id_pro = Convertor.ConvertToInt(pole_idprojektu3.Text);

            int id_ukl = Convertor.ConvertToInt(pole_id3.Text);

            if ((pole_problem3.Text.Length > 0 && pole_popis3.Text.Length > 0) && (pole_idprojektu3.Text.Length > 0 && pole_id3.Text.Length > 0))
            {
                String sql = "";
                Dictionary<object, object> pole = new Dictionary<object, object>();

                try
                {
                    sql = "INSERT INTO Bugy " + "(popis, id_projektu, id_ukolu, problem, kos, datum_mazani, datum_uplneho_mazani) VALUES"
                            + "(@popis, @id_projektu, @id_ukolu, @problem, @kos, @datum_mazani, @datum_uplneho_mazani)";

                    pole = new Dictionary<object, object>
                    {
                            {"@popis", pole_popis3.Text},
                            {"@id_projektu", id_pro},
                            {"@id_ukolu", id_ukl},
                            {"@problem", pole_problem3.Text},
                            {"@kos", false},
                            {"@datum_mazani", ""},
                            {"@datum_uplneho_mazani", ""}
                    };

                    if (DB.QueryVoid(pole, sql))
                    {
                        Zprava_ukolu.Text = "Přidán nový záznam o bugu!";
                    }
                }
                catch (Exception ex)
                {
                    System.Windows.MessageBox.Show(ex.Message);
                    return;
                }
                MenuItemClear();
                MenuItem5Button_Click(sender, e);

                pole_popis3.Text = string.Empty;
                pole_idprojektu3.Text = string.Empty;
                pole_id3.Text = string.Empty;
                pole_problem3.Text = string.Empty;

                sql = "SELECT * FROM Projekty WHERE id=" + id_pro;

                List<object> stav = DB.Query(sql);

                int actual_pocet = Convert.ConvertToInt((string)stav[9]);

                sql = "UPDATE dbo.Projekty " +
                             "SET pocet_bugy=@pocet_bugy " +
                             "WHERE id=" + id_pro;

                pole = new Dictionary<object, object>{
                            {"@pocet_bugy", actual_pocet+1}
                        };

                DB.QueryVoid(pole, sql);

                return;
            }
            else if (pole_popis3.Text.Length <= 0)
            {
                Zprava_ukolu.Text = "Nezadán žádný popis!";
            }
            else if (pole_problem3.Text.Length <= 0)
            {
                Zprava_ukolu.Text = "Nezadán žádný problém!";
            }
        }

        private void EditDBBug_Click(object sender, RoutedEventArgs e)
        {

            return;
        }

        private void EditDBError_Click(object sender, RoutedEventArgs e)
        {

            return;
        }

        private void AddDBError_Click(object sender, RoutedEventArgs e)
        {
            int id_pro = Convertor.ConvertToInt(pole_idprojektu4.Text);

            int id_ukl = Convertor.ConvertToInt(pole_id4.Text);

            if ((pole_problem4.Text.Length > 0 && pole_popis4.Text.Length > 0) && (pole_idprojektu4.Text.Length > 0 && pole_id4.Text.Length > 0))
            {
                String sql = "";
                Dictionary<object, object> pole = new Dictionary<object, object>();

                try
                {
                    sql = "INSERT INTO Problemy " + "(popis, id_projektu, id_ukolu, problem, kos, datum_mazani, datum_uplneho_mazani) VALUES"
                            + "(@popis, @id_projektu, @id_ukolu, @problem, @kos, @datum_mazani, @datum_uplneho_mazani)";

                    pole = new Dictionary<object, object>
                    {
                            {"@popis", pole_popis4.Text},
                            {"@id_projektu", id_pro},
                            {"@id_ukolu", id_ukl},
                            {"@problem", pole_problem4.Text},
                            {"@kos", false},
                            {"@datum_mazani", ""},
                            {"@datum_uplneho_mazani", ""}
                    };

                    if (DB.QueryVoid(pole, sql))
                    {
                        Zprava_ukolu.Text = "Přidán nový záznam o bugu!";
                    }
                }
                catch (Exception ex)
                {
                    System.Windows.MessageBox.Show(ex.Message);
                    return;
                }
                MenuItemClear();
                MenuItem4Button_Click(sender, e);

                pole_popis4.Text = string.Empty;
                pole_idprojektu4.Text = string.Empty;
                pole_id4.Text = string.Empty;
                pole_problem4.Text = string.Empty;

                sql = "SELECT * FROM Projekty WHERE id=" + id_pro;

                List<object> stav = DB.Query(sql);

                int actual_pocet = Convert.ConvertToInt((string)stav[10]);

                sql = "UPDATE dbo.Projekty " +
                             "SET pocet_problemy=@pocet_problemy " +
                             "WHERE id=" + id_pro;

                pole = new Dictionary<object, object>{
                            {"@pocet_problemy", actual_pocet+1}
                        };

                DB.QueryVoid(pole, sql);

                return;
            }
            else if (pole_popis4.Text.Length <= 0)
            {
                Zprava_ukolu.Text = "Nezadán žádný popis!";
            }
            else if (pole_problem4.Text.Length <= 0)
            {
                Zprava_ukolu.Text = "Nezadán žádný problém!";
            }
        }

        #region BinButton

        private void EmptyBin_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (ukoly_kos.Count > 0)
                {
                    foreach (var ukol in ukoly_kos)
                    {
                        String sql = "DELETE FROM dbo.Ukoly WHERE id='" + ukol.Id + "'";
                        DB.QueryVoid(null, sql);

                        sql = "UPDATE dbo.Projekty " +
                                     "SET pocet_komentare=@pocet_komentare " +
                                     "WHERE id=" + ukol.Id_projektu;

                        Dictionary<object, object> pole = new Dictionary<object, object>{
                            {"@pocet_komentare", 0}
                        };

                        DB.QueryVoid(pole, sql);
                    }
                }

                if (projekty_kos.Count > 0)
                {
                    foreach (var projekt in projekty_kos)
                    {
                        String sql = "DELETE FROM dbo.Projekty WHERE id='" + projekt.Id + "'";
                        DB.QueryVoid(null, sql);
                    }
                }

                if (problemy_kos.Count > 0)
                {
                    foreach (var problem in problemy_kos)
                    {
                        String sql = "DELETE FROM Problemy WHERE id='" + problem.Id + "'";
                        DB.QueryVoid(null, sql);

                        sql = "UPDATE dbo.Projekty " +
                                     "SET pocet_problemy=@pocet_problemy " +
                                     "WHERE id=" + problem.Id_projektu;

                        Dictionary<object, object> pole = new Dictionary<object, object>{
                            {"@pocet_problemy", 0}
                        };

                        DB.QueryVoid(pole, sql);
                    }
                }

                if (bugy_kos.Count > 0)
                {
                    foreach (var bug in bugy_kos)
                    {
                        String sql = "DELETE FROM Bugy WHERE id='" + bug.Id + "'";
                        DB.QueryVoid(null, sql);

                        sql = "UPDATE dbo.Projekty " +
                              "SET pocet_bugy=@pocet_bugy " +
                              "WHERE id=" + bug.Id_projektu;

                        Dictionary<object, object> pole = new Dictionary<object, object>{
                            {"@pocet_bugy", 0}
                        };

                        DB.QueryVoid(pole, sql);

                    }
                }
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(ex.Message);
            }

            BinButtonLoad_Click(sender, e);
        }

        private void BinButtonLoad_Click(object sender, RoutedEventArgs e)
        {
            MenuItemClear();
            tcSample.SelectedIndex = 11;
            BinLoad_Click(sender, e);
        }

        private void BinLoad_Click(object sender, RoutedEventArgs e)
        {
            Nahrat_Data(4, PoleHledat.Text, Abecedne);
            var bc = new BrushConverter();

            if (ukoly_kos.Count > 0)
            {
                for (int i = 0; i < ukoly_kos.Count; i++)
                {
                    System.Windows.Controls.Grid txtNumber = new System.Windows.Controls.Grid
                    {
                        Width = 755,
                        Height = 250,
                        Background = Brushes.DarkRed,
                        Margin = new Thickness(20)
                    };

                    System.Windows.Controls.Label nazev = new System.Windows.Controls.Label
                    {
                        Width = 650,
                        Height = 40,
                        FontSize = 20,
                        Margin = new Thickness(0, -200, 0, 0)
                    };

                    System.Windows.Controls.Label datum = new System.Windows.Controls.Label
                    {
                        Width = 220,
                        Height = 40,
                        FontSize = 18,
                        Foreground = Brushes.Gray,
                        Margin = new Thickness(-430, -150, 0, 0)
                    };

                    System.Windows.Controls.Image detail = new System.Windows.Controls.Image
                    {
                        Source = new BitmapImage(new Uri(@"Ikony/icons8_cancel.ico", UriKind.Relative)),
                        Margin = new Thickness(580, -200, 0, 0),
                        Width = 26,
                        Height = 26,
                        Name = "Ukc_" + i
                    };

                    System.Windows.Controls.Image detail2 = new System.Windows.Controls.Image
                    {
                        Source = new BitmapImage(new Uri(@"Ikony/icons8_sync.ico", UriKind.Relative)),
                        Margin = new Thickness(660, -200, 0, 0),
                        Width = 32,
                        Height = 32,
                        Name = "Ukc_" + i
                    };

                    detail2.MouseEnter += Detail_MouseEnter;
                    detail2.MouseLeftButtonDown += ReLoadButton2_Click;
                    detail.MouseEnter += Detail_MouseEnter;
                    detail.MouseLeftButtonDown += DetailCancel1_MouseLeftButtonDown;

                    nazev.Content = ukoly_kos[i].Nazev;
                    datum.Content = ukoly_kos[i].Datum_mazani + " - " + ukoly_kos[i].Datum_uplneho_mazani;

                    txtNumber.Children.Add(datum);
                    txtNumber.Children.Add(nazev);
                    txtNumber.Children.Add(detail);
                    txtNumber.Children.Add(detail2);
                    txtNumber.Name = "Ukl_" + i;

                    Kos_vypis.Children.Add(txtNumber);
                }
            }

            Nahrat_Data(3, PoleHledat.Text, Abecedne);

            if (projekty_kos.Count > 0)
            {
                for (int i = 0; i < projekty_kos.Count; i++)
                {
                    System.Windows.Controls.Grid txtNumber = new System.Windows.Controls.Grid
                    {
                        Width = 755,
                        Height = 250,
                        Background = Brushes.DarkRed,
                        Margin = new Thickness(20)
                    };

                    System.Windows.Controls.Label nazev = new System.Windows.Controls.Label();
                    System.Windows.Controls.Label datum = new System.Windows.Controls.Label();

                    System.Windows.Controls.Image detail = new System.Windows.Controls.Image
                    {
                        Source = new BitmapImage(new Uri(@"Ikony/icons8_cancel.ico", UriKind.Relative)),
                        Margin = new Thickness(580, -200, 0, 0),
                        Width = 26,
                        Height = 26,
                        Name = "Ukc_" + i
                    };

                    System.Windows.Controls.Image detail2 = new System.Windows.Controls.Image
                    {
                        Source = new BitmapImage(new Uri(@"Ikony/icons8_sync.ico", UriKind.Relative)),
                        Margin = new Thickness(660, -200, 0, 0),
                        Width = 32,
                        Height = 32,
                        Name = "Ukc_" + i
                    };

                    detail2.MouseEnter += Detail_MouseEnter;
                    detail2.MouseLeftButtonDown += ReLoadButton_Click;
                    detail.MouseEnter += Detail_MouseEnter;
                    detail.MouseLeftButtonDown += DetailCancel2_MouseLeftButtonDown;

                    nazev.Content = projekty_kos[i].Nazev;
                    datum.Content = projekty_kos[i].Datum_mazani + " - " + projekty_kos[i].Datum_uplneho_mazani;

                    nazev.Width = 650;
                    nazev.Height = 40;
                    nazev.FontSize = 20;
                    nazev.Margin = new Thickness(0, -200, 0, 0);

                    datum.Width = 220;
                    datum.Height = 40;
                    datum.FontSize = 18;
                    datum.Foreground = Brushes.Gray;
                    datum.Margin = new Thickness(-430, -150, 0, 0);

                    txtNumber.Children.Add(datum);
                    txtNumber.Children.Add(nazev);
                    txtNumber.Children.Add(detail);
                    txtNumber.Children.Add(detail2);
                    txtNumber.Name = "Pro_" + i;

                    Kos_vypis.Children.Add(txtNumber);
                }
            }

            Nahrat_Data(7, PoleHledat.Text, Abecedne);

            if (problemy_kos.Count > 0)
            {
                for (int i = 0; i < problemy_kos.Count; i++)
                {
                    System.Windows.Controls.Grid txtNumber = new System.Windows.Controls.Grid
                    {
                        Width = 755,
                        Height = 250,
                        Background = Brushes.DarkRed,
                        Margin = new Thickness(20)
                    };

                    System.Windows.Controls.Label nazev = new System.Windows.Controls.Label();
                    System.Windows.Controls.Label datum = new System.Windows.Controls.Label();

                    System.Windows.Controls.Image detail = new System.Windows.Controls.Image
                    {
                        Source = new BitmapImage(new Uri(@"Ikony/icons8_cancel.ico", UriKind.Relative)),
                        Margin = new Thickness(580, -200, 0, 0),
                        Width = 26,
                        Height = 26,
                        Name = "Ukc_" + i
                    };

                    System.Windows.Controls.Image detail2 = new System.Windows.Controls.Image
                    {
                        Source = new BitmapImage(new Uri(@"Ikony/icons8_sync.ico", UriKind.Relative)),
                        Margin = new Thickness(660, -200, 0, 0),
                        Width = 32,
                        Height = 32,
                        Name = "Ukc_" + i
                    };

                    nazev.Content = problemy_kos[i].Popis;
                    datum.Content = problemy_kos[i].Datum_mazani + " - " + problemy_kos[i].Datum_uplneho_mazani;

                    detail2.MouseLeftButtonDown += ReLoadButton3_Click;
                    detail2.MouseEnter += Detail_MouseEnter;

                    detail.MouseEnter += Detail_MouseEnter;
                    detail.MouseLeftButtonDown += DetailCancel3_MouseLeftButtonDown;

                    nazev.Width = 650;
                    nazev.Height = 40;
                    nazev.FontSize = 20;
                    nazev.Margin = new Thickness(0, -200, 0, 0);

                    datum.Width = 220;
                    datum.Height = 40;
                    datum.FontSize = 18;
                    datum.Foreground = Brushes.Gray;
                    datum.Margin = new Thickness(-430, -150, 0, 0);

                    txtNumber.Children.Add(datum);
                    txtNumber.Children.Add(nazev);
                    txtNumber.Children.Add(detail);
                    txtNumber.Children.Add(detail2);
                    txtNumber.Name = "Pro_" + i;

                    Kos_vypis.Children.Add(txtNumber);
                }
            }

            Nahrat_Data(8, PoleHledat.Text, Abecedne);

            if (bugy_kos.Count > 0)
            {
                for (int i = 0; i < bugy_kos.Count; i++)
                {
                    System.Windows.Controls.Grid txtNumber2 = new System.Windows.Controls.Grid
                    {
                        Width = 755,
                        Height = 250,
                        Background = Brushes.DarkRed,
                        Margin = new Thickness(20)
                    };

                    System.Windows.Controls.Label nazev = new System.Windows.Controls.Label();
                    System.Windows.Controls.Label datum = new System.Windows.Controls.Label();

                    System.Windows.Controls.Image detail = new System.Windows.Controls.Image
                    {
                        Source = new BitmapImage(new Uri(@"Ikony/icons8_cancel.ico", UriKind.Relative)),
                        Margin = new Thickness(580, -200, 0, 0),
                        Width = 26,
                        Height = 26,
                        Name = "Ukc_" + i
                    };

                    System.Windows.Controls.Image detail2 = new System.Windows.Controls.Image
                    {
                        Source = new BitmapImage(new Uri(@"Ikony/icons8_sync.ico", UriKind.Relative)),
                        Margin = new Thickness(660, -200, 0, 0),
                        Width = 32,
                        Height = 32,
                        Name = "Ukc_" + i
                    };

                    detail.MouseLeftButtonDown += DetailCancel4_MouseLeftButtonDown;
                    detail.MouseEnter += Detail_MouseEnter;

                    detail2.MouseLeftButtonDown += ReLoadButton4_Click;
                    detail2.MouseEnter += Detail_MouseEnter;

                    nazev.Content = bugy_kos[i].Popis;
                    datum.Content = bugy_kos[i].Datum_mazani + " - " + bugy_kos[i].Datum_uplneho_mazani;

                    nazev.Width = 650;
                    nazev.Height = 40;
                    nazev.FontSize = 20;
                    nazev.Margin = new Thickness(0, -200, 0, 0);

                    datum.Width = 220;
                    datum.Height = 40;
                    datum.FontSize = 18;
                    datum.Foreground = Brushes.Gray;
                    datum.Margin = new Thickness(-430, -150, 0, 0);

                    txtNumber2.Children.Add(datum);
                    txtNumber2.Children.Add(nazev);
                    txtNumber2.Children.Add(detail);
                    txtNumber2.Children.Add(detail2);
                    txtNumber2.Name = "Pro_" + i;

                    Kos_vypis.Children.Add(txtNumber2);
                }
            }
        }

        #endregion BinButton

        private void DetailCancel1_MouseLeftButtonDown(object sender, RoutedEventArgs e)
        {
            Image sender2 = (Image)sender;
            string numericString = string.Empty;

            foreach (var c in sender2.Name)
            {
                if ((c != 'U' && c != 'k') && (c != '_' && c != 'c'))
                {
                    numericString += c;
                }
            }

            int polozka_id = Int32.Parse(numericString);

            int id = ukoly_kos[polozka_id].Id;

            String sql = "DELETE FROM dbo.Ukoly WHERE id='" + id + "'";
            DB.QueryVoid(null, sql);

            sql = "SELECT * FROM Projekty WHERE id=" + ukoly_kos[polozka_id].Id_projektu;

            List<object> stav = DB.Query(sql);

            int actual_pocet = Convert.ConvertToInt((string)stav[8]);

            sql = "UPDATE dbo.Projekty " +
                         "SET pocet_komentare=@pocet_komentare " +
                         "WHERE id=" + ukoly_kos[polozka_id].Id_projektu;

            Dictionary<object, object> pole = new Dictionary<object, object>{
                            {"@pocet_komentare", actual_pocet-1}
                        };

            DB.QueryVoid(pole, sql);

            BinButtonLoad_Click(sender, e);
        }

        private void DetailCancel2_MouseLeftButtonDown(object sender, RoutedEventArgs e)
        {
            Image sender2 = (Image)sender;
            string numericString = string.Empty;

            foreach (var c in sender2.Name)
            {
                if ((c != 'U' && c != 'k') && (c != '_' && c != 'c'))
                {
                    numericString += c;
                }
            }

            int polozka_id = Int32.Parse(numericString);

            int id = projekty_kos[polozka_id].Id;

            String sql = "DELETE FROM dbo.Projekty WHERE id='" + id + "'";
            DB.QueryVoid(null, sql);
            BinButtonLoad_Click(sender, e);
        }

        private void DetailCancel3_MouseLeftButtonDown(object sender, RoutedEventArgs e)
        {
            Image sender2 = (Image)sender;
            string numericString = string.Empty;

            foreach (var c in sender2.Name)
            {
                if ((c != 'U' && c != 'k') && (c != '_' && c != 'c'))
                {
                    numericString += c;
                }
            }

            int polozka_id = Int32.Parse(numericString);

            int id = problemy_kos[polozka_id].Id;

            String sql = "DELETE FROM Problemy WHERE id='" + id + "'";
            DB.QueryVoid(null, sql);

            sql = "SELECT * FROM Projekty WHERE id=" + problemy_kos[polozka_id].Id_projektu;

            List<object> stav = DB.Query(sql);

            int actual_pocet = Convert.ConvertToInt((string)stav[10]);

            sql = "UPDATE dbo.Projekty " +
                         "SET pocet_problemy=@pocet_problemy " +
                         "WHERE id=" + problemy_kos[polozka_id].Id_projektu;

            Dictionary<object, object> pole = new Dictionary<object, object>{
                            {"@pocet_problemy", actual_pocet-1}
                        };

            DB.QueryVoid(pole, sql);

            BinButtonLoad_Click(sender, e);
        }

        private void DetailCancel4_MouseLeftButtonDown(object sender, RoutedEventArgs e)
        {
            Image sender2 = (Image)sender;
            string numericString = string.Empty;

            foreach (var c in sender2.Name)
            {
                if ((c != 'U' && c != 'k') && (c != '_' && c != 'c'))
                {
                    numericString += c;
                }
            }

            int polozka_id = Int32.Parse(numericString);

            int id = bugy_kos[polozka_id].Id;

            String sql = "DELETE FROM Bugy WHERE id='" + id + "'";
            DB.QueryVoid(null, sql);

            sql = "SELECT * FROM Projekty WHERE id=" + bugy_kos[polozka_id].Id_projektu;

            List<object> stav = DB.Query(sql);

            int actual_pocet = Convert.ConvertToInt((string)stav[9]);

            sql = "UPDATE dbo.Projekty " +
                         "SET pocet_bugy=@pocet_bugy " +
                         "WHERE id=" + bugy_kos[polozka_id].Id_projektu;

            Dictionary<object, object> pole = new Dictionary<object, object>{
                            {"@pocet_bugy", actual_pocet-1}
                        };

            DB.QueryVoid(pole, sql);

            BinButtonLoad_Click(sender, e);
        }

        private void ReLoadButton_Click(object sender, RoutedEventArgs e)
        {
            Image sender2 = (Image)sender;
            string numericString = string.Empty;

            foreach (var c in sender2.Name)
            {
                if ((c != 'U' && c != 'k') && (c != '_' && c != 'c'))
                {
                    numericString += c;
                }
            }

            int id = Int32.Parse(numericString);

            int a_id = projekty_kos[id].Id;

            try
            {
                String sql = "UPDATE dbo.Projekty " +
                             "SET kos=@kos, datum_mazani=@datum_mazani, datum_uplneho_mazani=@datum_uplneho_mazani " +
                             "WHERE id=" + a_id;

                Dictionary<object, object> pole = new Dictionary<object, object>{
                            {"@kos", false},
                            {"@datum_mazani", ""},
                            {"@datum_uplneho_mazani", ""}
                        };

                if (DB.QueryVoid(pole, sql))
                {
                    Zprava_ukolu.Text = "Položka obnovena z koše!";
                }

                BinButtonLoad_Click(sender, e);

            } catch (Exception)
            {
                BinButtonLoad_Click(sender, e);
            }
        }

        private void ReLoadButton2_Click(object sender, RoutedEventArgs e)
        {
            Image sender2 = (Image)sender;
            string numericString = string.Empty;

            foreach (var c in sender2.Name)
            {
                if ((c != 'U' && c != 'k') && (c != '_' && c != 'c'))
                {
                    numericString += c;
                }
            }

            int id = Int32.Parse(numericString);

            int a_id = ukoly_kos[id].Id;

            try
            {
                String sql = "UPDATE dbo.Ukoly " +
                             "SET kos=@kos, datum_mazani=@datum_mazani, datum_uplneho_mazani=@datum_uplneho_mazani " +
                             "WHERE id=" + a_id;

                Dictionary<object, object> pole = new Dictionary<object, object>{
                            {"@kos", false},
                            {"@datum_mazani", ""},
                            {"@datum_uplneho_mazani", ""}
                        };

                if (DB.QueryVoid(pole, sql))
                {
                    Zprava_ukolu.Text = "Položka obnovena z koše!";
                }

                BinButtonLoad_Click(sender, e);

            }
            catch (Exception)
            {
                BinButtonLoad_Click(sender, e);
            }
        }

        private void ReLoadButton3_Click(object sender, RoutedEventArgs e)
        {
            Image sender2 = (Image)sender;
            string numericString = string.Empty;

            foreach (var c in sender2.Name)
            {
                if ((c != 'U' && c != 'k') && (c != '_' && c != 'c'))
                {
                    numericString += c;
                }
            }

            int id = Int32.Parse(numericString);

            int a_id = problemy_kos[id].Id;

            try
            {
                String sql = "UPDATE Problemy " +
                             "SET kos=@kos, datum_mazani=@datum_mazani, datum_uplneho_mazani=@datum_uplneho_mazani " +
                             "WHERE id=" + a_id;

                Dictionary<object, object> pole = new Dictionary<object, object>{
                            {"@kos", false},
                            {"@datum_mazani", ""},
                            {"@datum_uplneho_mazani", ""}
                        };

                if (DB.QueryVoid(pole, sql))
                {
                    Zprava_ukolu.Text = "Položka obnovena z koše!";
                }

                BinButtonLoad_Click(sender, e);

            }
            catch (Exception)
            {
                BinButtonLoad_Click(sender, e);
            }
        }

        private void ReLoadButton4_Click(object sender, RoutedEventArgs e)
        {
            Image sender2 = (Image)sender;
            string numericString = string.Empty;

            foreach (var c in sender2.Name)
            {
                if ((c != 'U' && c != 'k') && (c != '_' && c != 'c'))
                {
                    numericString += c;
                }
            }

            int id = Int32.Parse(numericString);

            int a_id = bugy_kos[id].Id;

            try
            {
                String sql = "UPDATE dbo.Bugy " +
                             "SET kos=@kos, datum_mazani=@datum_mazani, datum_uplneho_mazani=@datum_uplneho_mazani " +
                             "WHERE id=" + a_id;

                Dictionary<object, object> pole = new Dictionary<object, object>{
                            {"@kos", false},
                            {"@datum_mazani", ""},
                            {"@datum_uplneho_mazani", ""}
                        };

                if (DB.QueryVoid(pole, sql))
                {
                    Zprava_ukolu.Text = "Položka obnovena z koše!";
                }

                BinButtonLoad_Click(sender, e);

            }
            catch (Exception)
            {
                BinButtonLoad_Click(sender, e);
            }
        }

        private void AddNewProjectButton_Click(object sender, RoutedEventArgs e)
        {
            MenuItem1Button_Click(sender, e);
            tcSample.SelectedIndex = 6;
        }

        private void AddNewTaskButton_Click(object sender, RoutedEventArgs e)
        {
            MenuItem1Button_Click(sender, e);
            ID_projektu.Items.Clear();
            foreach (var projekt in projekty)
            {
                ComboBoxItem item = new ComboBoxItem
                {
                    Content = projekt.Id + ". " + projekt.Nazev
                };
                ID_projektu.Items.Add(item);
            }

            if (Counter.CountRow(1) > 0)
            {
                MenuItem2Button_Click(sender, e);
                tcSample.SelectedIndex = 7;
            }
            else
            {
                Zprava_ukolu.Text = "Nejprve vytvořte nový projekt!";
            }
        }

        #region EditDBDetail
        private void EditDBDetail_Click(object sender, RoutedEventArgs e)
        {
            Button sender2 = (Button)sender;
            string numericString = string.Empty;

            foreach (var c in sender2.Name)
            {
                if ((c != 'E' && c != 'd') && c != '_')
                {
                    numericString += c;
                }
            }

            int table_id = Int32.Parse(numericString);

            try
            {
                String sql = string.Empty;

                if (table_id == 1)
                {
                    sql = "UPDATE dbo.Projekty " +
                                 "SET nazev=@nazev, datum_vytvoreni=@datum_vytvoreni, jmeno_klienta=@jmeno_klienta, zadani=@zadani, technologie=@technologie, " +
                                 "datum_odevzdani=@datum_odevzdani, cas_vypracovani=@cas_vypracovani " +
                                 "WHERE id=" + Aktivni_ID;

                    Dictionary<object, object> pole2 = new Dictionary<object, object>{
                            {"@nazev", pole_nazev1.Text},
                            {"@datum_vytvoreni", pole_datum_vytv1.Text},
                            {"@jmeno_klienta", pole_jmeno1.Text},
                            {"@zadani", pole_zadani1.Text},
                            {"@technologie", pole_technologie1.Text },
                            {"@datum_odevzdani", pole_datum_odevzd1.Text},
                            {"@cas_vypracovani", pole_cas_vypr1.Text}
                        };

                    if (DB.QueryVoid(pole2, sql))
                    {
                        Zprava_ukolu.Text = "Úspěšně přidán nový projekt!";
                    }

                    MenuItem1Button_Click(sender, e);

                    return;

                }
                else if (table_id == 2)
                {
                    sql = "UPDATE dbo.Ukoly " +
                                 "SET nazev=@nazev, datum_zadani=@datum_zadani, zadani=@zadani, cas_vypracovani=@cas_vypracovani, datum_odevzdani=@datum_odevzdani, " +
                                 "komentare=@komentare, id_projektu=@id_projektu " +
                                 "WHERE id=" + Aktivni_ID;

                    Dictionary<object, object> pole2 = new Dictionary<object, object>{
                            {"@nazev", pole_nazev2.Text},
                            {"@datum_zadani", pole_datum_zadani2.Text},
                            {"@zadani", pole_zadani2.Text},
                            {"@cas_vypracovani", pole_cas_vypr2.Text},
                            {"@datum_odevzdani", pole_datum_vypr2.Text},
                            {"@komentare", pole_komentare2.Text},
                            {"@id_projektu", pole_idprojektu2.Text}
                        };

                    if (DB.QueryVoid(pole2, sql))
                    {
                        Zprava_ukolu.Text = "Úspěšně přidán nový úkol!";
                    }

                    MenuItem2Button_Click(sender, e);

                    return;
                }
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(ex.Message);
                return;
            }

            return;
        }

        private void ActionDnes1_Click(object sender, RoutedEventArgs e)
        {
            pole_datum_vytv1.Text = DateTime.Now.ToString("dd.MM.yyyy");
        }

        private void ActionDnes2_Click(object sender, RoutedEventArgs e)
        {
            pole_datum_zadani2.Text = DateTime.Now.ToString("dd.MM.yyyy");
        }

        private void ActionDnes3_Click(object sender, RoutedEventArgs e)
        {
            Datum_zadani.Text = DateTime.Now.ToString("dd.MM.yyyy");
        }
        private void ActionDnes4_Click(object sender, RoutedEventArgs e)
        {
            Datum_vytvoreni.Text = DateTime.Now.ToString("dd.MM.yyyy");
        }

        public void PoleHledat_GotKeyboardFocus(object sender, RoutedEventArgs e)
        {
            if (PoleHledat.Text == "Vyhledat ...")
            {
                PoleHledat.Text = "";
                PoleHledat.Foreground = Brushes.Black;
            }
        }

        #endregion EditDBDetail

        #region MenuItemClear
        private void MenuItemClear()
        {
            var bc = new BrushConverter();
            MenuItem1.Background = (Brush)bc.ConvertFrom("#323A4E");
            MenuItem2.Background = (Brush)bc.ConvertFrom("#323A4E");
            MenuItem3.Background = (Brush)bc.ConvertFrom("#323A4E");
            MenuItem4.Background = (Brush)bc.ConvertFrom("#323A4E");
            MenuItem5.Background = (Brush)bc.ConvertFrom("#323A4E");

            Projekty_vypis.Children.Clear();
            Ukoly_vypis.Children.Clear();
            Kos_vypis.Children.Clear();
            Problemy_vypis.Children.Clear();
            Bugy_vypis.Children.Clear();

            Grid_detail_projekt.Background = Brushes.White;
            Grid_detail_projekt3.Background = Brushes.White;

            Grid_detail_ukol.Background = Brushes.White;
            Grid_detail_ukol2.Background = Brushes.White;

            if (PoleHledat.Text == "")
            {
                PoleHledat.Text = "Vyhledat ...";
                PoleHledat.Foreground = Brushes.Gray;
            }

            MenuItem10.BorderThickness = new Thickness(0, 0, 0, 0);
            MenuItem10.Margin = new Thickness(0, 0, 0, 0);
            MenuItem20.BorderThickness = new Thickness(0, 0, 0, 0);
            MenuItem20.Margin = new Thickness(0, 0, 0, 0);
            MenuItem30.BorderThickness = new Thickness(0, 0, 0, 0);
            MenuItem30.Margin = new Thickness(0, 0, 0, 0);
            MenuItem40.BorderThickness = new Thickness(0, 0, 0, 0);
            MenuItem40.Margin = new Thickness(0, 0, 0, 0);
            MenuItem50.BorderThickness = new Thickness(0, 0, 0, 0);
            MenuItem50.Margin = new Thickness(0, 0, 0, 0);
            Icon1.Source = new BitmapImage(new Uri(@"Ikony/icons8_add_to_database_1.ico", UriKind.Relative));
            Icon2.Source = new BitmapImage(new Uri(@"Ikony/icons8_System_Task_1.ico", UriKind.Relative));
            Icon3.Source = new BitmapImage(new Uri(@"Ikony/icons8_Calendar_31.ico", UriKind.Relative));
            Icon4.Source = new BitmapImage(new Uri(@"Ikony/icons8_error_1.ico", UriKind.Relative));
            Icon5.Source = new BitmapImage(new Uri(@"Ikony/icons8_bug.ico", UriKind.Relative));
            Icon6.Source = new BitmapImage(new Uri(@"Ikony/icons8_Trash_Can_3.ico", UriKind.Relative));
        }

        #endregion MenuItemClear

        #region MenuItem1Button
        public void MenuItem1Button_Click(object sender, RoutedEventArgs e)
        {
            Aktivni_Polozka = 1;
            MenuItemClear();
            tcSample.SelectedIndex = 0;
            var bc = new BrushConverter();
            MenuItem1.Background = (Brush)bc.ConvertFrom("#037C7C");
            MenuItem10.BorderBrush = new SolidColorBrush(Colors.Red);
            MenuItem10.BorderThickness = new Thickness(2, 0, 0, 0);
            MenuItem10.Margin = new Thickness(-2, 0, 0, 0);

            Icon1.Source = new BitmapImage(new Uri(@"Ikony/icons8_add_to_database_2.ico", UriKind.Relative));
            Nahrat_Data(1, PoleHledat.Text, Abecedne);

            if (projekty.Count > 0)
            {
                for (int i = 0; i < projekty.Count; i++)
                {
                    System.Windows.Controls.Grid txtNumber = new System.Windows.Controls.Grid
                    {
                        Width = 755,
                        Height = 250,
                        Background = Brushes.White,
                        Margin = new Thickness(20)
                    };

                    Image img1 = new Image
                    {
                        Source = new BitmapImage(new Uri(@"Ikony/icons8_document_1.ico", UriKind.Relative)),
                        Margin = new Thickness(0, 0, 0, 0),
                        Height = 68,
                        Width = 68
                    };

                    System.Windows.Controls.Label nazev = new System.Windows.Controls.Label();
                    System.Windows.Controls.Label datum = new System.Windows.Controls.Label();

                    nazev.Content = projekty[i].Nazev;
                    datum.Content = projekty[i].Datum_vytvoreni + " - " + projekty[i].Datum_odevzdani;

                    nazev.Width = 650;
                    nazev.Height = 40;
                    nazev.FontSize = 20;
                    nazev.Margin = new Thickness(0, -200, 0, 0);

                    datum.Width = 220;
                    datum.Height = 40;
                    datum.FontSize = 18;
                    datum.Foreground = Brushes.Gray;
                    datum.Margin = new Thickness(-430, -150, 0, 0);

                    Image img2 = new Image
                    {
                        Source = new BitmapImage(new Uri(@"Ikony/icons8_error_1.ico", UriKind.Relative)),
                        Margin = new Thickness(-500, 200, 0, 0),
                        Height = 28,
                        Width = 28
                    };

                    img2.MouseLeftButtonDown += MenuItem4Button_Click;
                    img2.MouseEnter += Img2_MouseEnter;

                    Image img3 = new Image
                    {
                        Source = new BitmapImage(new Uri(@"Ikony/icons8_bug.ico", UriKind.Relative)),
                        Margin = new Thickness(0, 200, 0, 0),
                        Height = 28,
                        Width = 28
                    };

                    img3.MouseLeftButtonDown += MenuItem5Button_Click;
                    img3.MouseEnter += Img3_MouseEnter;

                    Image img4 = new Image
                    {
                        Source = new BitmapImage(new Uri(@"Ikony/icons8_System_Task_1.ico", UriKind.Relative)),
                        Margin = new Thickness(500, 200, 0, 0),
                        Height = 28,
                        Width = 28
                    };

                    img4.MouseLeftButtonDown += MenuItem2Button_Click;
                    img4.MouseEnter += Img4_MouseEnter;

                    System.Windows.Controls.Label bugy_label = new System.Windows.Controls.Label();
                    System.Windows.Controls.Label problemy_label = new System.Windows.Controls.Label();
                    System.Windows.Controls.Label koment_label = new System.Windows.Controls.Label();

                    System.Windows.Controls.Image detail = new System.Windows.Controls.Image
                    {
                        Source = new BitmapImage(new Uri(@"Ikony/icons8_edit.ico", UriKind.Relative)),
                        Margin = new Thickness(500, -200, 0, 0),
                        Width = 22,
                        Height = 22,
                        Name = "Pro_" + i
                    };

                    detail.MouseEnter += Detail_MouseEnter;
                    detail.MouseLeftButtonDown += Detail1_MouseDoubleClick;

                    bugy_label.Content = projekty[i].Pocet_bugy;
                    problemy_label.Content = projekty[i].Pocet_problemy;
                    koment_label.Content = projekty[i].Pocet_komentare;


                    bugy_label.Width = 28;
                    bugy_label.Height = 28;
                    bugy_label.FontSize = 16;

                    problemy_label.FontSize = 16;
                    problemy_label.Width = 28;
                    problemy_label.Height = 28;

                    koment_label.FontSize = 16;
                    koment_label.Width = 28;
                    koment_label.Height = 28;

                    if (projekty[i].Pocet_bugy != "0")
                    {
                        bugy_label.Foreground = Brushes.Red;
                    }
                    else
                    {
                        bugy_label.Foreground = Brushes.DarkGray;
                    }

                    if (projekty[i].Pocet_problemy != "0")
                    {
                        problemy_label.Foreground = Brushes.Red;
                    }
                    else
                    {
                        problemy_label.Foreground = Brushes.DarkGray;
                    }

                    bugy_label.Margin = new Thickness(-65, 200, 0, 0);
                    problemy_label.Margin = new Thickness(-565, 200, 0, 0);
                    koment_label.Margin = new Thickness(435, 200, 0, 0);

                    System.Windows.Controls.Button parent_butt = new System.Windows.Controls.Button
                    {
                        Width = 755,
                        Height = 250,
                        Name = "Bu_" + i,
                        BorderThickness = new Thickness(0, 0, 0, 0),
                        Background = Brushes.White
                    };

                    if (projekty[i].Hotovo)
                    {
                        parent_butt.Background = (Brush)bc.ConvertFrom("#00FFA7");
                    }


                    parent_butt.Click += VybratPolozku_Click;


                    System.Windows.Controls.Image detail2 = new System.Windows.Controls.Image
                    {
                        Source = new BitmapImage(new Uri(@"Ikony/icons8_delete.ico", UriKind.Relative)),
                        Margin = new Thickness(580, -200, 0, 0),
                        Width = 26,
                        Height = 26,
                        Name = "Ukl_" + i
                    };

                    detail2.MouseEnter += Detail_MouseEnter;
                    detail2.MouseLeftButtonDown += DetailDelete_MouseDoubleClick;

                    txtNumber.Children.Add(parent_butt);

                    txtNumber.Children.Add(img1);
                    txtNumber.Children.Add(nazev);
                    txtNumber.Children.Add(datum);
                    txtNumber.Children.Add(img2);
                    txtNumber.Children.Add(img3);
                    txtNumber.Children.Add(img4);
                    txtNumber.Children.Add(bugy_label);
                    txtNumber.Children.Add(problemy_label);
                    txtNumber.Children.Add(koment_label);
                    txtNumber.Children.Add(detail);
                    txtNumber.Children.Add(detail2);

                    txtNumber.Name = "Pro_" + i;

                    Projekty_vypis.Children.Add(txtNumber);
                }
            }
        }

        private void Img2_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            Image obr1 = (Image)sender;

            obr1.Cursor = Cursors.Hand;
        }

        private void Img3_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            Image obr2 = (Image)sender;

            obr2.Cursor = Cursors.Hand;
        }

        private void Img4_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            Image obr3 = (Image)sender;

            obr3.Cursor = Cursors.Hand;
        }

        #endregion MenuItem1Button

        #region VybratPolozku
        private void VybratPolozku_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            Button sender1 = ((Button)sender);
            string numericString = string.Empty;

            foreach (var c in sender1.Name)
            {
                if ((c != 'B' && c != 'u') && c != '_')
                {
                    numericString += c;
                }
            }

            int i = Int32.Parse(numericString);

            if (Projekty_vypis.Children.Count > 0)
            {
                if (Projekty_vypis.Children[i].Opacity != 0.5)
                {
                    Projekty_vypis.Children[i].Opacity = 0.5;
                }
                else
                {
                    Projekty_vypis.Children[i].Opacity = 1;
                }
            }
        }

        private void VybratPolozku2_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            Button sender1 = ((Button)sender);
            string numericString = string.Empty;

            foreach (var c in sender1.Name)
            {
                if ((c != 'B' && c != 'u') && c != '_')
                {
                    numericString += c;
                }
            }

            int i = Int32.Parse(numericString);

            if (Ukoly_vypis.Children.Count > 0)
            {
                if (Ukoly_vypis.Children[i].Opacity != 0.5)
                {
                    Ukoly_vypis.Children[i].Opacity = 0.5;
                }
                else
                {
                    Ukoly_vypis.Children[i].Opacity = 1;
                }
            }
        }

        private void VybratPolozku3_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            Button sender1 = ((Button)sender);
            string numericString = string.Empty;

            foreach (var c in sender1.Name)
            {
                if ((c != 'B' && c != 'u') && c != '_')
                {
                    numericString += c;
                }
            }

            int i = Int32.Parse(numericString);

            if (Problemy_vypis.Children.Count > 0)
            {
                if (Problemy_vypis.Children[i].Opacity != 0.5)
                {
                    Problemy_vypis.Children[i].Opacity = 0.5;
                }
                else
                {
                    Problemy_vypis.Children[i].Opacity = 1;
                }
            }
        }

        private void VybratPolozku4_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            Button sender1 = ((Button)sender);
            string numericString = string.Empty;

            foreach (var c in sender1.Name)
            {
                if ((c != 'B' && c != 'u') && c != '_')
                {
                    numericString += c;
                }
            }

            int i = Int32.Parse(numericString);

            if (Bugy_vypis.Children.Count > 0)
            {
                if (Bugy_vypis.Children[i].Opacity != 0.5)
                {
                    Bugy_vypis.Children[i].Opacity = 0.5;
                }
                else
                {
                    Bugy_vypis.Children[i].Opacity = 1;
                }
            }
        }
        #endregion VybratPolozku

        #region MenuItem2
        public void MenuItem2Button_Click(object sender, RoutedEventArgs e)
        {
            Aktivni_Polozka = 2;
            MenuItemClear();
            tcSample.SelectedIndex = 1;
            var bc = new BrushConverter();
            MenuItem2.Background = (Brush)bc.ConvertFrom("#037C7C");
            MenuItem20.BorderBrush = new SolidColorBrush(Colors.Red);
            MenuItem20.BorderThickness = new Thickness(2, 0, 0, 0);
            MenuItem20.Margin = new Thickness(-2, 0, 0, 0);

            Icon2.Source = new BitmapImage(new Uri(@"Ikony/icons8_System_Task.ico", UriKind.Relative));

            Nahrat_Data(2, PoleHledat.Text, Abecedne);

            if (ukoly.Count > 0)
            {
                for (int i = 0; i < ukoly.Count; i++)
                {
                    System.Windows.Controls.Grid txtNumber = new System.Windows.Controls.Grid
                    {
                        Width = 755,
                        Height = 250,
                        Background = (Brush)bc.ConvertFrom("#00FFA7"),
                        Margin = new Thickness(20)
                    };

                    Image img1 = new Image
                    {
                        Source = new BitmapImage(new Uri(@"Ikony/icons8_System_Task_1.ico", UriKind.Relative)),
                        Margin = new Thickness(0, 0, 0, 0),
                        Height = 68,
                        Width = 68
                    };

                    System.Windows.Controls.Button parent_butt = new System.Windows.Controls.Button
                    {
                        Width = 755,
                        Height = 250,
                        Name = "Bu_" + i,
                        BorderThickness = new Thickness(0, 0, 0, 0),
                        Background = Brushes.White
                    };

                    System.Windows.Controls.Label nazev = new System.Windows.Controls.Label();
                    System.Windows.Controls.Label datum = new System.Windows.Controls.Label();

                    System.Windows.Controls.Image detail = new System.Windows.Controls.Image
                    {
                        Source = new BitmapImage(new Uri(@"Ikony/icons8_edit.ico", UriKind.Relative)),
                        Margin = new Thickness(500, -200, 0, 0),
                        Width = 22,
                        Height = 22,
                        Name = "Ukl_" + i
                    };

                    detail.MouseEnter += Detail_MouseEnter;
                    detail.MouseLeftButtonDown += Detail2_MouseDoubleClick;

                    System.Windows.Controls.Image detail2 = new System.Windows.Controls.Image
                    {
                        Source = new BitmapImage(new Uri(@"Ikony/icons8_delete.ico", UriKind.Relative)),
                        Margin = new Thickness(580, -200, 0, 0),
                        Width = 26,
                        Height = 26,
                        Name = "Ukc_" + i
                    };

                    detail2.MouseEnter += Detail_MouseEnter;
                    detail2.MouseLeftButtonDown += DetailDelete2_MouseDoubleClick;

                    nazev.Content = ukoly[i].Nazev;
                    datum.Content = ukoly[i].Datum_zadani + " - " + ukoly[i].Datum_odevzdani;

                    nazev.Width = 650;
                    nazev.Height = 40;
                    nazev.FontSize = 20;
                    nazev.Margin = new Thickness(0, -200, 0, 0);

                    datum.Width = 220;
                    datum.Height = 40;
                    datum.FontSize = 18;
                    datum.Foreground = Brushes.Gray;
                    datum.Margin = new Thickness(-430, -150, 0, 0);

                    parent_butt.Click += VybratPolozku2_Click;

                    txtNumber.Children.Add(parent_butt);
                    txtNumber.Children.Add(datum);
                    txtNumber.Children.Add(nazev);
                    txtNumber.Children.Add(img1);
                    txtNumber.Children.Add(detail);
                    txtNumber.Children.Add(detail2);

                    txtNumber.Name = "Ukl_" + i;

                    Ukoly_vypis.Children.Add(txtNumber);
                }
            }
        }

        #endregion MenuItem2

        #region DetailClick

        private void DetailDelete_MouseDoubleClick(object sender, System.Windows.Input.MouseEventArgs e)
        {
            System.Windows.Controls.Image sender1 = ((System.Windows.Controls.Image)sender);
            string numericString = string.Empty;

            foreach (var c in sender1.Name)
            {
                if ((c != 'U' && c != 'k') && (c != '_' && c != 'l'))
                {
                    numericString += c;
                }
            }

            int id = Int32.Parse(numericString);

            Aktivni_ID = projekty[id].Id;

            try
            {
                String sql =    "UPDATE dbo.Projekty " +
                                "SET kos=@kos, datum_mazani=@datum_mazani, datum_uplneho_mazani=@datum_uplneho_mazani " +
                                "WHERE id=" + Aktivni_ID;

                Dictionary<object, object> pole = new Dictionary<object, object>{
                            {"@kos", true},
                            {"@datum_mazani", DateTime.Now.ToString("dd.MM.yyyy")},
                            {"@datum_uplneho_mazani", DateTime.Now.AddDays(15).ToString("dd.MM.yyyy")}
                        };

                if (DB.QueryVoid(pole, sql))
                {
                    Zprava_ukolu.Text = "Položka smazána do koše!";
                }

            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(ex.Message);
            }

            MenuItem1Button_Click(sender, e);
        }

        private void DetailDelete2_MouseDoubleClick(object sender, System.Windows.Input.MouseEventArgs e)
        {
            System.Windows.Controls.Image sender1 = ((System.Windows.Controls.Image)sender);
            string numericString = string.Empty;

            foreach (var c in sender1.Name)
            {
                if ((c != 'U' && c != 'k') && (c != '_' && c != 'c'))
                {
                    numericString += c;
                }
            }

            int id = Int32.Parse(numericString);

            Aktivni_ID = ukoly[id].Id;

            try
            {
                String sql = "UPDATE dbo.Ukoly " +
                "SET kos=@kos, datum_mazani=@datum_mazani, datum_uplneho_mazani=@datum_uplneho_mazani " +
                "WHERE id=" + Aktivni_ID;

                Dictionary<object, object> pole = new Dictionary<object, object>{
                            {"@kos", true},
                            {"@datum_mazani", DateTime.Now.ToString("dd.MM.yyyy")},
                            {"@datum_uplneho_mazani", DateTime.Now.AddDays(15).ToString("dd.MM.yyyy")}
                        };

                if (DB.QueryVoid(pole, sql))
                {
                    Zprava_ukolu.Text = "Položka smazána do koše!";
                }

            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(ex.Message);
            }

            MenuItem2Button_Click(sender, e);
        }

        private void DetailEdit3_MouseDoubleClick(object sender, System.Windows.Input.MouseEventArgs e)
        {
            Aktivni_Polozka = 13;
            MenuItemClear();
            MenuItem4Button_Click(sender, e);
            tcSample.SelectedIndex = 14;
        }

        private void DetailEdit4_MouseDoubleClick(object sender, System.Windows.Input.MouseEventArgs e)
        {
            Aktivni_Polozka = 14;
            MenuItemClear();
            MenuItem5Button_Click(sender, e);
            tcSample.SelectedIndex = 15 ;
        }

        private void DetailDelete3_MouseDoubleClick(object sender, System.Windows.Input.MouseEventArgs e)
        {
            System.Windows.Controls.Image sender1 = ((System.Windows.Controls.Image)sender);
            string numericString = string.Empty;

            foreach (var c in sender1.Name)
            {
                if ((c != 'U' && c != 'k') && (c != '_' && c != 'c'))
                {
                    numericString += c;
                }
            }

            int id = Int32.Parse(numericString);

            Aktivni_ID = problemy[id].Id;

            try
            {
                String sql = "UPDATE Problemy " +
                "SET kos=@kos, datum_mazani=@datum_mazani, datum_uplneho_mazani=@datum_uplneho_mazani " +
                "WHERE id=" + Aktivni_ID;

                Dictionary<object, object> pole = new Dictionary<object, object>{
                            {"@kos", true},
                            {"@datum_mazani", DateTime.Now.ToString("dd.MM.yyyy")},
                            {"@datum_uplneho_mazani", DateTime.Now.AddDays(15).ToString("dd.MM.yyyy")}
                        };

                if (DB.QueryVoid(pole, sql))
                {
                    Zprava_ukolu.Text = "Položka smazána do koše!";
                }

            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(ex.Message);
            }

            MenuItem4Button_Click(sender, e);
        }

        private void DetailDelete4_MouseDoubleClick(object sender, System.Windows.Input.MouseEventArgs e)
        {
            System.Windows.Controls.Image sender1 = ((System.Windows.Controls.Image)sender);
            string numericString = string.Empty;

            foreach (var c in sender1.Name)
            {
                if ((c != 'U' && c != 'k') && (c != '_' && c != 'c'))
                {
                    numericString += c;
                }
            }

            int id = Int32.Parse(numericString);

            Aktivni_ID = bugy[id].Id;

            try
            {
                String sql = "UPDATE Bugy " +
                "SET kos=@kos, datum_mazani=@datum_mazani, datum_uplneho_mazani=@datum_uplneho_mazani " +
                "WHERE id=" + Aktivni_ID;

                Dictionary<object, object> pole = new Dictionary<object, object>{
                            {"@kos", true},
                            {"@datum_mazani", DateTime.Now.ToString("dd.MM.yyyy")},
                            {"@datum_uplneho_mazani", DateTime.Now.AddDays(15).ToString("dd.MM.yyyy")}
                        };

                if (DB.QueryVoid(pole, sql))
                {
                    Zprava_ukolu.Text = "Položka smazána do koše!";
                }

            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(ex.Message);
            }

            MenuItem5Button_Click(sender, e);
        }

        private void Detail_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            System.Windows.Controls.Image obr = (System.Windows.Controls.Image)sender;

            obr.Cursor = Cursors.Hand;
        }

        private void Detail1_MouseDoubleClick(object sender, System.Windows.Input.MouseEventArgs e)
        {
            System.Windows.Controls.Image sender1 = ((System.Windows.Controls.Image)sender);
            string numericString = string.Empty;

            foreach (var c in sender1.Name)
            {
                if ((c != 'P' && c != 'r') && (c != '_' && c != 'o'))
                {
                    numericString += c;
                }
            }

            int i = Int32.Parse(numericString);

            Aktivni_ID = projekty[i].Id;

            MenuItemClear();
            tcSample.SelectedIndex = 9;
            Nadpis_detail1.Content = "Detail - Projekt " + projekty[i].Id;

            if (projekty[i].Hotovo)
            {
                Grid_detail_projekt3.Background = Brushes.LightGray;
                Grid_detail_projekt2.Background = Brushes.LightGray;
            }
            else
            {
                Grid_detail_projekt3.Background = Brushes.White;
                Grid_detail_projekt2.Background = Brushes.White;
            }

            pole_id1.Text = projekty[i].Id.ToString();
            pole_nazev1.Text = projekty[i].Nazev;
            pole_datum_vytv1.Text = projekty[i].Datum_vytvoreni;
            pole_datum_odevzd1.Text = projekty[i].Datum_odevzdani;
            pole_zadani1.Text = projekty[i].Zadani;
            pole_technologie1.Text = projekty[i].Technologie;
            pole_jmeno1.Text = projekty[i].Jmeno_klienta;
            pole_cas_vypr1.Text = projekty[i].Cas_vypracovani;

            Pocet_bugy1.Content = projekty[i].Pocet_bugy;
            Pocet_komentare1.Content = projekty[i].Pocet_komentare;
            Pocet_problemy1.Content = projekty[i].Pocet_problemy;
        }

        private void Detail2_MouseDoubleClick(object sender, System.Windows.Input.MouseEventArgs e)
        {
            System.Windows.Controls.Image sender1 = ((System.Windows.Controls.Image)sender);
            string numericString = string.Empty;

            foreach (var c in sender1.Name)
            {
                if ((c != 'U' && c != 'k') && (c != '_' && c != 'l'))
                {
                    numericString += c;
                }
            }

            int i = Int32.Parse(numericString);

            Aktivni_ID = ukoly[i].Id;

            MenuItemClear();
            tcSample.SelectedIndex = 10;
            Nadpis_detail2.Content = "Detail - Úkol " + ukoly[i].Id;

            if (ukoly[i].Hotovo)
            {
                Grid_detail_ukol2.Background = Brushes.LightGray;
                Grid_detail_ukol.Background = Brushes.LightGray;
            }
            else
            {
                Grid_detail_ukol2.Background = Brushes.White;
                Grid_detail_ukol.Background = Brushes.White;
            }

            pole_id2.Text = ukoly[i].Id.ToString();
            pole_cas_vypr2.Text = ukoly[i].Cas_vypracovani;
            pole_datum_vypr2.Text = ukoly[i].Datum_odevzdani;
            pole_datum_zadani2.Text = ukoly[i].Datum_zadani;
            pole_idprojektu2.Text = ukoly[i].Id_projektu.ToString();
            pole_komentare2.Text = ukoly[i].Komentare;
            pole_zadani2.Text = ukoly[i].Zadani;
            pole_nazev2.Text = ukoly[i].Nazev;
            pole_sazba2.Text = ukoly[i].Sazba.ToString();
            pole_pocet2.Text = ukoly[i].Pocet_hodin.ToString();
            pole_vypocet2.Text = (ukoly[i].Pocet_hodin * ukoly[i].Sazba).ToString() + " " + ukoly[i].Mena.ToString();
        }

        #endregion DetailClick

        #region MenuItemClicks

        private void MenuItem3Button_Click(object sender, RoutedEventArgs e)
        {
            MenuItemClear();
            Zprava_ukolu.Text = "";
            tcSample.SelectedIndex = 2;
            var bc = new BrushConverter();
            MenuItem3.Background = (Brush)bc.ConvertFrom("#037C7C");
            MenuItem30.BorderBrush = new SolidColorBrush(Colors.Red);
            MenuItem30.BorderThickness = new Thickness(2, 0, 0, 0);
            MenuItem30.Margin = new Thickness(-2, 0, 0, 0);

            Icon3.Source = new BitmapImage(new Uri(@"Ikony/icons8_Calendar_31_1.ico", UriKind.Relative));
        }

        private void MenuItem4Button_Click(object sender, RoutedEventArgs e)
        {
            MenuItemClear();
            Aktivni_Polozka = 4;
            Zprava_ukolu.Text = "";
            tcSample.SelectedIndex = 3;
            var bc = new BrushConverter();
            MenuItem4.Background = (Brush)bc.ConvertFrom("#037C7C");
            MenuItem40.BorderBrush = new SolidColorBrush(Colors.Red);
            MenuItem40.BorderThickness = new Thickness(2, 0, 0, 0);
            MenuItem40.Margin = new Thickness(-2, 0, 0, 0);

            Icon4.Source = new BitmapImage(new Uri(@"Ikony/icons8_error.ico", UriKind.Relative));

            Nahrat_Data(5, PoleHledat.Text, Abecedne);

            if (problemy.Count > 0)
            {
                for (int i = 0; i < problemy.Count; i++)
                {
                    System.Windows.Controls.Grid txtNumber = new System.Windows.Controls.Grid
                    {
                        Width = 755,
                        Height = 250,
                        Background = (Brush)bc.ConvertFrom("#00FFA7"),
                        Margin = new Thickness(20)
                    };

                    System.Windows.Controls.Button parent_butt = new System.Windows.Controls.Button
                    {
                        Width = 755,
                        Height = 250,
                        Name = "Bu_" + i,
                        BorderThickness = new Thickness(0, 0, 0, 0),
                        Background = Brushes.Pink
                    };

                    System.Windows.Controls.Label nazev = new System.Windows.Controls.Label
                    {
                        Content = i+1 + ". " + problemy[i].Problem,

                        Width = 650,
                        Height = 40,
                        FontSize = 20,
                        Margin = new Thickness(0, -200, 0, 0)
                    };

                    Image img1 = new Image
                    {
                        Source = new BitmapImage(new Uri(@"Ikony/icons8_error_1.ico", UriKind.Relative)),
                        Margin = new Thickness(0, 0, 0, 0),
                        Height = 68,
                        Width = 68
                    };

                    System.Windows.Controls.Image detail = new System.Windows.Controls.Image
                    {
                        Source = new BitmapImage(new Uri(@"Ikony/icons8_edit.ico", UriKind.Relative)),
                        Margin = new Thickness(500, -200, 0, 0),
                        Width = 22,
                        Height = 22,
                        Name = "Pro_" + i
                    };

                    System.Windows.Controls.Image detail2 = new System.Windows.Controls.Image
                    {
                        Source = new BitmapImage(new Uri(@"Ikony/icons8_delete.ico", UriKind.Relative)),
                        Margin = new Thickness(580, -200, 0, 0),
                        Width = 26,
                        Height = 26,
                        Name = "Ukc_" + i
                    };

                    System.Windows.Controls.Label popisek1 = new System.Windows.Controls.Label
                    {
                        Width = 650,
                        Height = 40,
                        FontSize = 20,
                        Margin = new Thickness(0, 130, 0, 0)
                    };

                    System.Windows.Controls.Label popisek2 = new System.Windows.Controls.Label
                    {
                        Width = 650,
                        Height = 40,
                        FontSize = 20,
                        Margin = new Thickness(0, 180, 0, 0)
                    };

                    int id_pro = problemy[i].Id_projektu;
                    int id_ukl = problemy[i].Id_ukolu;

                    String sql = "SELECT * FROM Projekty WHERE Id='" + id_pro + "'";
                    String sql2 = "SELECT * FROM Ukoly WHERE Id='" + id_ukl + "'";

                    List<object> pole = DB.Query(sql);
                    List<object> pole2 = DB.Query(sql2);

                    if (pole != null && pole.Count > 0)
                    {
                        popisek1.Content = pole[1];
                    }

                    if (pole2 != null && pole2.Count > 0)
                    {
                        popisek2.Content = pole2[1];
                    }

                    detail.MouseEnter += Detail_MouseEnter;
                    detail.MouseLeftButtonDown += DetailEdit3_MouseDoubleClick;

                    detail2.MouseEnter += Detail_MouseEnter;
                    detail2.MouseLeftButtonDown += DetailDelete3_MouseDoubleClick;

                    parent_butt.Click += VybratPolozku3_Click;

                    txtNumber.Children.Add(parent_butt);
                    txtNumber.Children.Add(img1);
                    txtNumber.Children.Add(popisek1);
                    txtNumber.Children.Add(popisek2);
                    txtNumber.Children.Add(nazev);
                    txtNumber.Children.Add(detail);
                    txtNumber.Children.Add(detail2);

                    Problemy_vypis.Children.Add(txtNumber);
                }
            }
        }
        private void MenuItem5Button_Click(object sender, RoutedEventArgs e)
        {
            Aktivni_Polozka = 5;
            MenuItemClear();
            Zprava_ukolu.Text = "";
            tcSample.SelectedIndex = 4;
            var bc = new BrushConverter();
            MenuItem5.Background = (Brush)bc.ConvertFrom("#037C7C");
            MenuItem50.BorderBrush = new SolidColorBrush(Colors.Red);
            MenuItem50.BorderThickness = new Thickness(2, 0, 0, 0);
            MenuItem50.Margin = new Thickness(-2, 0, 0, 0);

            Icon5.Source = new BitmapImage(new Uri(@"Ikony/icons8_bug_1.ico", UriKind.Relative));
            
            Nahrat_Data(6, PoleHledat.Text, Abecedne);

            if (bugy.Count > 0)
            {
                Bugy_vypis.Children.Clear();
                for (int i = 0; i < bugy.Count; i++)
                {
                    System.Windows.Controls.Grid txtNumber = new System.Windows.Controls.Grid
                    {
                        Width = 755,
                        Height = 250,
                        Background = (Brush)bc.ConvertFrom("#00FFA7"),
                        Margin = new Thickness(20)
                    };

                    System.Windows.Controls.Button parent_butt = new System.Windows.Controls.Button
                    {
                        Width = 755,
                        Height = 250,
                        Name = "Bu_" + i,
                        BorderThickness = new Thickness(0, 0, 0, 0),
                        Background = Brushes.Pink
                    };

                    System.Windows.Controls.Label nazev = new System.Windows.Controls.Label
                    {
                        Content = i+1 + ". " + bugy[i].Problem,

                        Width = 650,
                        Height = 40,
                        FontSize = 20,
                        Margin = new Thickness(0, -200, 0, 0)
                    };

                    Image img1 = new Image
                    {
                        Source = new BitmapImage(new Uri(@"Ikony/icons8_bug.ico", UriKind.Relative)),
                        Margin = new Thickness(0, 0, 0, 0),
                        Height = 68,
                        Width = 68
                    };

                    System.Windows.Controls.Image detail = new System.Windows.Controls.Image
                    {
                        Source = new BitmapImage(new Uri(@"Ikony/icons8_edit.ico", UriKind.Relative)),
                        Margin = new Thickness(500, -200, 0, 0),
                        Width = 22,
                        Height = 22,
                        Name = "Pro_" + i
                    };

                    System.Windows.Controls.Image detail2 = new System.Windows.Controls.Image
                    {
                        Source = new BitmapImage(new Uri(@"Ikony/icons8_delete.ico", UriKind.Relative)),
                        Margin = new Thickness(580, -200, 0, 0),
                        Width = 26,
                        Height = 26,
                        Name = "Ukc_" + i
                    };

                    System.Windows.Controls.Label popisek1 = new System.Windows.Controls.Label
                    {
                        Width = 650,
                        Height = 40,
                        FontSize = 20,
                        Margin = new Thickness(0, 130, 0, 0)
                    };

                    System.Windows.Controls.Label popisek2 = new System.Windows.Controls.Label
                    {
                        Width = 650,
                        Height = 40,
                        FontSize = 20,
                        Margin = new Thickness(0, 180, 0, 0)
                    };

                    int id_pro = bugy[i].Id_projektu;
                    int id_ukl = bugy[i].Id_ukolu;

                    String sql = "SELECT * FROM Projekty WHERE Id='" + id_pro + "'";
                    String sql2 = "SELECT * FROM Ukoly WHERE Id='" + id_ukl + "'";

                    List<object> pole = DB.Query(sql);
                    List<object> pole2 = DB.Query(sql2);

                    if (pole != null && pole.Count > 0)
                    {
                        popisek1.Content = pole[1];
                    }

                    if (pole2 != null && pole2.Count > 0)
                    {
                        popisek2.Content = pole2[1];
                    }
                    
                    detail.MouseEnter += Detail_MouseEnter;
                    detail.MouseLeftButtonDown += DetailEdit4_MouseDoubleClick;

                    detail2.MouseEnter += Detail_MouseEnter;
                    detail2.MouseLeftButtonDown += DetailDelete4_MouseDoubleClick;

                    parent_butt.Click += VybratPolozku4_Click;

                    txtNumber.Children.Add(parent_butt);
                    txtNumber.Children.Add(img1);
                    txtNumber.Children.Add(nazev);
                    txtNumber.Children.Add(popisek1);
                    txtNumber.Children.Add(popisek2);
                    txtNumber.Children.Add(detail);
                    txtNumber.Children.Add(detail2);

                    Bugy_vypis.Children.Add(txtNumber);
                }
            }
        }

        private void MenuItem6Button_Click(object sender, RoutedEventArgs e)
        {
            MenuItemClear();
            Zprava_ukolu.Text = "";
            tcSample.SelectedIndex = 5;

            Icon6.Source = new BitmapImage(new Uri(@"Ikony/icons8_Trash_Can_2.ico", UriKind.Relative));
        }

        #endregion MenuItemClicks

        #region DeleteButton

        private void DeleteButton4_Click(object sender, RoutedEventArgs e)
        {
            int id = 0;
            foreach (UIElement indexer in Bugy_vypis.Children)
            {
                if (indexer.Opacity == 0.5)
                {
                    try
                    {
                        String sql = "UPDATE Bugy " +
                                        "SET kos=@kos, datum_mazani=@datum_mazani, datum_uplneho_mazani=@datum_uplneho_mazani " +
                                        "WHERE id=" + bugy[id].Id;

                        Dictionary<object, object> pole = new Dictionary<object, object>{
                            {"@kos", true},
                            {"@datum_mazani", DateTime.Now.ToString("dd.MM.yyyy")},
                            {"@datum_uplneho_mazani", DateTime.Now.AddDays(15).ToString("dd.MM.yyyy")}
                        };

                        if (DB.QueryVoid(pole, sql))
                        {
                            Zprava_ukolu.Text = "Položky smazány do koše!";
                        };
                    }
                    catch (Exception ex)
                    {
                        System.Windows.MessageBox.Show(ex.Message);
                    }
                }

                id++;
            }
            MenuItem5Button_Click(sender, e);
            return;
        }

        private void DeleteButton3_Click(object sender, RoutedEventArgs e)
        {
            int id = 0;
            foreach (UIElement indexer in Problemy_vypis.Children)
            {
                if (indexer.Opacity == 0.5)
                {
                    try
                    {
                        String sql = "UPDATE Problemy " +
                                        "SET kos=@kos, datum_mazani=@datum_mazani, datum_uplneho_mazani=@datum_uplneho_mazani " +
                                        "WHERE id=" + problemy[id].Id;

                        Dictionary<object, object> pole = new Dictionary<object, object>{
                            {"@kos", true},
                            {"@datum_mazani", DateTime.Now.ToString("dd.MM.yyyy")},
                            {"@datum_uplneho_mazani", DateTime.Now.AddDays(15).ToString("dd.MM.yyyy")}
                        };

                        if (DB.QueryVoid(pole, sql))
                        {
                            Zprava_ukolu.Text = "Položky smazány do koše!";
                        };
                    }
                    catch (Exception ex)
                    {
                        System.Windows.MessageBox.Show(ex.Message);
                    }
                }

                id++;
            }
            MenuItem4Button_Click(sender, e);
            return;
        }

        private void DeleteButton2_Click(object sender, RoutedEventArgs e)
        {
            int id = 0;
            foreach (UIElement indexer in Ukoly_vypis.Children)
            {
                if (indexer.Opacity == 0.5)
                {
                    try
                    {
                        String sql =    "UPDATE dbo.Ukoly " +
                                        "SET kos=@kos, datum_mazani=@datum_mazani, datum_uplneho_mazani=@datum_uplneho_mazani " +
                                        "WHERE id=" + ukoly[id].Id;

                        Dictionary<object, object> pole = new Dictionary<object, object>{
                            {"@kos", true},
                            {"@datum_mazani", DateTime.Now.ToString("dd.MM.yyyy")},
                            {"@datum_uplneho_mazani", DateTime.Now.AddDays(15).ToString("dd.MM.yyyy")}
                        };

                        if (DB.QueryVoid(pole, sql))
                        {
                            Zprava_ukolu.Text = "Položky smazány do koše!";
                        };
                    }
                    catch (Exception ex)
                    {
                        System.Windows.MessageBox.Show(ex.Message);
                    }
                }

                id++;
            }
            MenuItem2Button_Click(sender, e);
            return;
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            int id = 0;
            foreach (UIElement indexer in Projekty_vypis.Children)
            {
                if (indexer.Opacity == 0.5)
                {
                        String sql =    "UPDATE dbo.Projekty " +
                                        "SET kos=@kos, datum_mazani=@datum_mazani, datum_uplneho_mazani=@datum_uplneho_mazani " +
                                        "WHERE id=" + projekty[id].Id;

                        Dictionary<object, object> pole = new Dictionary<object, object>{
                            {"@kos", true},
                            {"@datum_mazani", DateTime.Now.ToString("dd.MM.yyyy")},
                            {"@datum_uplneho_mazani", DateTime.Now.AddDays(15).ToString("dd.MM.yyyy")}
                        };

                        if (DB.QueryVoid(pole, sql))
                        {
                            Zprava_ukolu.Text = "Položky smazány do koše!";
                        };
                }

                id++;
            }
            MenuItem1Button_Click(sender, e);
        }

        #endregion DeleteButton

        #region SelectAll

        private void SelectAllButton_Click(object sender, RoutedEventArgs e)
        {
            if (Projekty_vypis.Children.Count > 0)
            {
                bool proved = true;

                for (int i = 0; i < Projekty_vypis.Children.Count; i++)
                {
                    if (Projekty_vypis.Children[i].Opacity == 0.5)
                    {
                        proved = false;
                    }
                }

                if (proved)
                {
                    int id = 0;
                    foreach (UIElement indexer in Projekty_vypis.Children)
                    {
                        indexer.Opacity = 0.5;
                        id++;
                    }
                }
                else
                {
                    int id = 0;
                    foreach (UIElement indexer in Projekty_vypis.Children)
                    {
                        indexer.Opacity = 1;
                        id++;
                    }
                }
            }
        }

        private void SelectAllButton2_Click(object sender, RoutedEventArgs e)
        {
            if (Ukoly_vypis.Children.Count > 0)
            {
                bool proved = true;

                for (int i = 0; i < Ukoly_vypis.Children.Count; i++)
                {
                    if (Ukoly_vypis.Children[i].Opacity == 0.5)
                    {
                        proved = false;
                    }
                }

                if (proved)
                {
                    int id = 0;
                    foreach (UIElement indexer in Ukoly_vypis.Children)
                    {
                        indexer.Opacity = 0.5;
                        id++;
                    }
                }
                else
                {
                    int id = 0;
                    foreach (UIElement indexer in Ukoly_vypis.Children)
                    {
                        indexer.Opacity = 1;
                        id++;
                    }
                }
            }
        }

        private void SelectAllButton4_Click(object sender, RoutedEventArgs e)
        {
            if (Bugy_vypis.Children.Count > 0)
            {
                bool proved = true;

                for (int i = 0; i < Bugy_vypis.Children.Count; i++)
                {
                    if (Bugy_vypis.Children[i].Opacity == 0.5)
                    {
                        proved = false;
                    }
                }

                if (proved)
                {
                    int id = 0;
                    foreach (UIElement indexer in Bugy_vypis.Children)
                    {
                        indexer.Opacity = 0.5;
                        id++;
                    }
                }
                else
                {
                    int id = 0;
                    foreach (UIElement indexer in Bugy_vypis.Children)
                    {
                        indexer.Opacity = 1;
                        id++;
                    }
                }
            }
        }

        private void SelectAllButton3_Click(object sender, RoutedEventArgs e)
        {
            if (Problemy_vypis.Children.Count > 0)
            {
                bool proved = true;

                for (int i = 0; i < Problemy_vypis.Children.Count; i++)
                {
                    if (Problemy_vypis.Children[i].Opacity == 0.5)
                    {
                        proved = false;
                    }
                }

                if (proved)
                {
                    int id = 0;
                    foreach (UIElement indexer in Problemy_vypis.Children)
                    {
                        indexer.Opacity = 0.5;
                        id++;
                    }
                }
                else
                {
                    int id = 0;
                    foreach (UIElement indexer in Problemy_vypis.Children)
                    {
                        indexer.Opacity = 1;
                        id++;
                    }
                }
            }
        }
        #endregion SelectAll

        #region RazeniAbecedne
        private void AbecedneButton4_Click(object sender, RoutedEventArgs e)
        {
            if (Abecedne == false)
            {
                Abecedne = true;
            }
            else
            {
                Abecedne = false;
            }

            MenuItem5Button_Click(sender, e);
        }

        private void AbecedneButton3_Click(object sender, RoutedEventArgs e)
        {
            if (Abecedne == false)
            {
                Abecedne = true;
            }
            else
            {
                Abecedne = false;
            }

            MenuItem4Button_Click(sender, e);
        }

        private void AbecedneButton2_Click(object sender, RoutedEventArgs e)
        {
            if (Abecedne == false)
            {
                Abecedne = true;
            }
            else
            {
                Abecedne = false;
            }

            MenuItem2Button_Click(sender, e);
        }

        private void AbecedneButton_Click(object sender, RoutedEventArgs e)
        {
            if (Abecedne == false)
            {
                Abecedne = true;
            }
            else
            {
                Abecedne = false;
            }

            MenuItem1Button_Click(sender, e);
        }
        #endregion RazeniAbecedne

        #region NahratData

        public class Projekty_polozky
        {
            public String Nazev;
            public String Datum_vytvoreni;
            public String Jmeno_klienta;
            public String Zadani;
            public String Technologie;
            public String Datum_odevzdani;
            public String Cas_vypracovani;
            public String Pocet_komentare;
            public String Pocet_bugy;
            public String Pocet_problemy;
            public String Id_bugy;
            public String Id_problemy;
            public int Id;
            public int Sazba;
            public int Pocet_hodin;
            public bool Hotovo;
            public bool Kos;
        }

        public class Projekty_kos : Projekty_polozky
        {
            public String Datum_mazani;
            public String Datum_uplneho_mazani;
        }

        public class Problemy
        {
            public String Popis;
            public String Problem;
            public int Id_projektu;
            public int Id_ukolu;
            public int Id;
            public bool Kos;
        }

        public class Problemy_kos : Problemy
        {
            public String Datum_mazani;
            public String Datum_uplneho_mazani;
        }

        public class Ukoly_polozky
        {
            public String Nazev;
            public String Datum_zadani;
            public String Zadani;
            public String Cas_vypracovani;
            public String Datum_odevzdani;
            public String Komentare;
            public String Mena;
            public int Id_projektu;
            public int Id;
            public int Sazba;
            public int Pocet_hodin;
            public bool Hotovo;
            public bool Kos;
        }

        public class Ukoly_kos : Ukoly_polozky
        {
            public String Datum_mazani;
            public String Datum_uplneho_mazani;
        }

        public string Hledat { get; set; }

        public List<Projekty_polozky> projekty = new List<Projekty_polozky>();
        public List<Ukoly_polozky> ukoly = new List<Ukoly_polozky>();
        public List<Ukoly_kos> ukoly_kos = new List<Ukoly_kos>();
        public List<Projekty_kos> projekty_kos = new List<Projekty_kos>();
        public List<Problemy> problemy = new List<Problemy>();
        public List<Problemy_kos> problemy_kos = new List<Problemy_kos>();
        public List<Problemy> bugy = new List<Problemy>();
        public List<Problemy_kos> bugy_kos = new List<Problemy_kos>();

        public List<int> pocty = new List<int>();
        public List<int> pocty2 = new List<int>();
        public List<int> pocty3 = new List<int>();
        public List<int> pocty4 = new List<int>();
        public List<int> pocty5 = new List<int>();
        public List<int> pocty6 = new List<int>();
        public List<int> pocty7 = new List<int>();
        public List<int> pocty8 = new List<int>();

        public void Nahrat_Data(int table_id, string hledat, bool sorting)
        {
            if (hledat == "Vyhledat ...")
            {
                hledat = "";
            }

            if (table_id == 8)
            {
                bugy_kos.Clear();
                pocty8.Clear();
            }

            if (table_id == 7)
            {
                problemy_kos.Clear();
                pocty7.Clear();
            }

            if (table_id == 6)
            {
                bugy.Clear();
                pocty6.Clear();
            }

            if (table_id == 5)
            {
                problemy.Clear();
                pocty5.Clear();
            }

            if (table_id == 4)
            {
                pocty4.Clear();
                ukoly_kos.Clear();
            }

            if (table_id == 3)
            {
                projekty_kos.Clear();
                pocty3.Clear();
            }

            if (table_id == 2)
            {
                ukoly.Clear();
                pocty2.Clear();
            }

            if (table_id == 1)
            {
                pocty.Clear();
                projekty.Clear();
            }

            int poctyi = Counter.CountRow(table_id);

            int plus = 0;

            for (int i = 0; i < poctyi; i++)
            {
                try
                {
                    String sql = string.Empty;

                    if (table_id == 1)
                    {
                        sql = "SELECT * FROM Projekty WHERE nazev LIKE '" + hledat + "%' OR jmeno_klienta LIKE '" + hledat + "%' OR zadani LIKE '" + hledat + "%' OR technologie LIKE '" + hledat + "%'";
                    }
                    else if (table_id == 2)
                    {
                        sql = "SELECT * FROM Ukoly WHERE nazev LIKE '" + hledat + "%' OR zadani LIKE '" + hledat + "%' OR komentare LIKE '" + hledat + "%'";
                    }
                    else if (table_id == 3)
                    {
                        sql = "SELECT * FROM Projekty WHERE nazev LIKE '" + hledat + "%' AND kos='TRUE'";
                    }
                    else if (table_id == 4)
                    {
                        sql = "SELECT * FROM Ukoly WHERE nazev LIKE '" + hledat + "%' AND kos='TRUE'";
                    }
                    else if (table_id == 5)
                    {
                        sql = "SELECT * FROM Problemy WHERE popis LIKE '" + hledat + "%' AND kos='FALSE'";
                    }
                    else if (table_id == 6)
                    {
                        sql = "SELECT * FROM Bugy WHERE popis LIKE '" + hledat + "%' AND kos='FALSE'";
                    }
                    else if (table_id == 7)
                    {
                        sql = "SELECT * FROM Problemy WHERE popis LIKE '" + hledat + "%' AND kos='TRUE'";
                    }
                    else if (table_id == 8)
                    {
                        sql = "SELECT * FROM Bugy WHERE popis LIKE '" + hledat + "%' AND kos='TRUE'";
                    }
                    else { return; }

                    if (sorting && (table_id > 0 && table_id < 5))
                    {
                        sql += "ORDER BY nazev ASC";
                    }
                    else if (sorting && table_id > 4)
                    {
                        sql += "ORDER BY popis ASC";
                    }

                    List<object> pole = DB.Query(sql);

                    if (pole != null)
                    {
                        if (table_id == 1)
                        {
                            if (!((bool)pole[16 + plus]))
                            {
                                Projekty_polozky projekty_polozka = new Projekty_polozky
                                {
                                    Id = (Int32)pole[0 + plus],
                                    Nazev = (string)pole[1 + plus],
                                    Datum_vytvoreni = (string)pole[2 + plus],
                                    Jmeno_klienta = (string)pole[3 + plus],
                                    Zadani = (string)pole[4 + plus],
                                    Technologie = (string)pole[5 + plus],
                                    Datum_odevzdani = (string)pole[6 + plus],
                                    Cas_vypracovani = (string)pole[7 + plus],
                                    Pocet_komentare = (string)pole[8 + plus],
                                    Pocet_bugy = (string)pole[9 + plus],
                                    Pocet_problemy = (string)pole[10 + plus],
                                    Id_problemy = (string)pole[11 + plus],
                                    Id_bugy = (string)pole[12 + plus],
                                    Hotovo = (bool)pole[13 + plus],
                                    Sazba = (Int32)pole[14 + plus],
                                    Pocet_hodin = (Int32)pole[15 + plus],
                                    Kos = (bool)pole[16 + plus]
                                };

                                if (!pocty.Contains(projekty_polozka.Id))
                                {
                                    projekty.Add(projekty_polozka);
                                    pocty.Add(projekty_polozka.Id);
                                }
                            }
                            plus += 19;
                        }
                        else if (table_id == 2)
                        {
                            if (!((bool)pole[12 + plus]))
                            {
                                Ukoly_polozky ukol_polozka = new Ukoly_polozky
                                {
                                    Id = (Int32)pole[0 + plus],
                                    Nazev = (string)pole[1 + plus],
                                    Datum_zadani = (string)pole[2 + plus],
                                    Zadani = (string)pole[3 + plus],
                                    Cas_vypracovani = (string)pole[4 + plus],
                                    Datum_odevzdani = (string)pole[5 + plus],
                                    Komentare = (string)pole[6 + plus],
                                    Id_projektu = (Int32)pole[7 + plus],
                                    Hotovo = (bool)pole[8 + plus],
                                    Sazba = (Int32)pole[9 + plus],
                                    Pocet_hodin = (Int32)pole[10 + plus],
                                    Mena = (string)pole[11 + plus],
                                    Kos = (bool)pole[12 + plus]
                                };

                                if (!pocty2.Contains(ukol_polozka.Id))
                                {
                                    ukoly.Add(ukol_polozka);
                                    pocty2.Add(ukol_polozka.Id);
                                }
                            }
                            plus += 15;
                        }
                        else if (table_id == 3)
                        {
                            Projekty_kos projekt_kos = new Projekty_kos
                            {
                                Id = (Int32)pole[0 + plus],
                                Nazev = (string)pole[1 + plus],
                                Datum_vytvoreni = (string)pole[2 + plus],
                                Jmeno_klienta = (string)pole[3 + plus],
                                Zadani = (string)pole[4 + plus],
                                Technologie = (string)pole[5 + plus],
                                Datum_odevzdani = (string)pole[6 + plus],
                                Cas_vypracovani = (string)pole[7 + plus],
                                Pocet_komentare = (string)pole[8 + plus],
                                Pocet_bugy = (string)pole[9 + plus],
                                Pocet_problemy = (string)pole[10 + plus],
                                Id_problemy = (string)pole[11 + plus],
                                Id_bugy = (string)pole[12 + plus],
                                Hotovo = (bool)pole[13 + plus],
                                Sazba = (Int32)pole[14 + plus],
                                Pocet_hodin = (Int32)pole[15 + plus],
                                Kos = (bool)pole[16 + plus],
                                Datum_mazani = (string)pole[17 + plus],
                                Datum_uplneho_mazani = (string)pole[18 + plus]
                            };

                            if (!pocty3.Contains(projekt_kos.Id))
                            {
                                projekty_kos.Add(projekt_kos);
                                pocty3.Add(projekt_kos.Id);
                            }
                            plus += 19;
                        }
                        else if (table_id == 4)
                        {
                            Ukoly_kos ukol_kos = new Ukoly_kos
                            {
                                Id = (Int32)pole[0 + plus],
                                Nazev = (string)pole[1 + plus],
                                Datum_zadani = (string)pole[2 + plus],
                                Zadani = (string)pole[3 + plus],
                                Cas_vypracovani = (string)pole[4 + plus],
                                Datum_odevzdani = (string)pole[5 + plus],
                                Komentare = (string)pole[6 + plus],
                                Id_projektu = (Int32)pole[7 + plus],
                                Hotovo = (bool)pole[8 + plus],
                                Sazba = (Int32)pole[9 + plus],
                                Pocet_hodin = (Int32)pole[10 + plus],
                                Mena = (string)pole[11 + plus],
                                Kos = (bool)pole[12 + plus],
                                Datum_mazani = (string)pole[13 + plus],
                                Datum_uplneho_mazani = (string)pole[14 + plus]
                            };

                            if (!pocty4.Contains(ukol_kos.Id))
                            {
                                ukoly_kos.Add(ukol_kos);
                                pocty4.Add(ukol_kos.Id);
                            }
                            plus += 15;
                        }
                        else if (table_id == 5)
                        {
                            Problemy problem = new Problemy
                            {
                                Id = (Int32)pole[0 + plus],
                                Popis = (string)pole[1 + plus],
                                Id_projektu = (Int32)pole[2 + plus],
                                Id_ukolu = (Int32)pole[3 + plus],
                                Problem = (string)pole[4 + plus],
                                Kos = (bool)pole[5 + plus]
                            };

                            if (!pocty5.Contains(problem.Id))
                            {
                                problemy.Add(problem);
                                pocty5.Add(problem.Id);
                            }
                            plus += 8;
                        }
                        else if (table_id == 6)
                        {
                            Problemy bug = new Problemy
                            {
                                Id = (Int32)pole[0 + plus],
                                Popis = (string)pole[1 + plus],
                                Id_projektu = (Int32)pole[2 + plus],
                                Id_ukolu = (Int32)pole[3 + plus],
                                Problem = (string)pole[4 + plus],
                                Kos = (bool)pole[5 + plus]
                            };

                            if (!pocty6.Contains(bug.Id))
                            {
                                bugy.Add(bug);
                                pocty6.Add(bug.Id);
                            }
                            plus += 8;
                        }
                        else if (table_id == 7)
                        {
                            Problemy_kos problem = new Problemy_kos
                            {
                                Id = (Int32)pole[0 + plus],
                                Popis = (string)pole[1 + plus],
                                Id_projektu = (Int32)pole[2 + plus],
                                Id_ukolu = (Int32)pole[3 + plus],
                                Problem = (string)pole[4 + plus],
                                Kos = (bool)pole[5 + plus],
                                Datum_mazani = (string)pole[6 + plus],
                                Datum_uplneho_mazani = (string)pole[7 + plus]
                            };

                            if (!pocty7.Contains(problem.Id))
                            {
                                problemy_kos.Add(problem);
                                pocty7.Add(problem.Id);
                            }
                            plus += 8;
                        }
                        else if (table_id == 8)
                        {
                            Problemy_kos bug = new Problemy_kos
                            {
                                Id = (Int32)pole[0 + plus],
                                Popis = (string)pole[1 + plus],
                                Id_projektu = (Int32)pole[2 + plus],
                                Id_ukolu = (Int32)pole[3 + plus],
                                Problem = (string)pole[4 + plus],
                                Kos = (bool)pole[5 + plus],
                                Datum_mazani = (string)pole[6 + plus],
                                Datum_uplneho_mazani = (string)pole[7 + plus]
                            };

                            if (!pocty8.Contains(bug.Id))
                            {
                                bugy_kos.Add(bug);
                                pocty8.Add(bug.Id);
                            }
                            plus += 8;
                        }
                        else { return; }
                    }
                }
                catch (Exception)
                {

                }
            }

            return;
        }

        #endregion NahratData

        #region AddToDB

        public String VyberMena
        {
            get; set;
        }

        private void AddToDBTaskButton_Click(object sender, RoutedEventArgs e)
        {
            double pocet_dnu = 0;
            if (VyberMena == null || VyberMena == "")
            {
                VyberMena = " Kč";
            }
            try
            {
                pocet_dnu = (Convertor.ConvertToDate(Datum_odevzdani2.Text) - Convertor.ConvertToDate(Datum_zadani.Text)).TotalDays;
            }
            catch { }

            if (Nazev_ukolu.Text.Length > 0 && Zadani_ukolu.Text.Length > 0)
            {
                int id_pro = Convertor.ConvertToInt(ID_projektu.Text);

                try
                {
                    String sql = "INSERT INTO Ukoly " + "(nazev, datum_zadani, zadani, cas_vypracovani, datum_odevzdani, komentare, id_projektu, hotovo, sazba, pocet_hodin, mena, kos, datum_mazani, datum_uplneho_mazani) VALUES"
                            + "(@nazev, @datum_zadani, @zadani, @cas_vypracovani, @datum_odevzdani, @komentare, @id_projektu, @hotovo, @sazba, @pocet_hodin, @mena, @kos, @datum_mazani, @datum_uplneho_mazani)";

                    string vystup_pocet_dnu = "";

                    if (pocet_dnu > 1 && pocet_dnu < 5)
                    {
                        vystup_pocet_dnu += pocet_dnu;
                        vystup_pocet_dnu += " dny";
                    }
                    else if (pocet_dnu == 1)
                    {
                        vystup_pocet_dnu += pocet_dnu;
                        vystup_pocet_dnu += " den";
                    }
                    else if (pocet_dnu > 4)
                    {
                        vystup_pocet_dnu += pocet_dnu;
                        vystup_pocet_dnu += " dnů";
                    }

                    Dictionary<object, object> pole = new Dictionary<object, object>
                    {
                            {"@nazev", Nazev_ukolu.Text},
                            {"@datum_zadani", Datum_zadani.Text},
                            {"@zadani", Zadani_ukolu.Text},
                            {"@cas_vypracovani", vystup_pocet_dnu},
                            {"@datum_odevzdani", Datum_odevzdani2.Text},
                            {"@komentare", Komentare.Text},
                            {"@id_projektu", id_pro},
                            {"@hotovo", 0},
                            {"@sazba", Int32.Parse(Sazba1.Text)},
                            {"@pocet_hodin", 0},
                            {"@mena", VyberMena},
                            {"@kos", false},
                            {"@datum_mazani", ""},
                            {"@datum_uplneho_mazani", ""}
                    };

                    if (DB.QueryVoid(pole, sql))
                    {
                        Zprava_ukolu.Text = "Přidán nový úkol!";
                    }

                    sql = "SELECT * FROM Projekty WHERE id=" + id_pro;

                    List<object> stav = DB.Query(sql);

                    int actual_pocet = Convert.ConvertToInt((string)stav[8]);
                    
                    sql = "UPDATE dbo.Projekty " +
                                 "SET pocet_komentare=@pocet_komentare " +
                                 "WHERE id=" + id_pro;

                    pole = new Dictionary<object, object>{
                            {"@pocet_komentare", actual_pocet+1}
                        };

                    DB.QueryVoid(pole, sql);

                }
                catch (Exception ex)
                {
                    System.Windows.MessageBox.Show(ex.Message);
                    return;
                }
                MenuItemClear();
                MenuItem2Button_Click(sender, e);

                tcSample.SelectedIndex = 1;
                Nazev_ukolu.Text = string.Empty;
                Datum_zadani.Text = string.Empty;
                Zadani_ukolu.Text = string.Empty;
                Datum_odevzdani2.Text = string.Empty;
                Komentare.Text = string.Empty;
                ID_projektu.Text = string.Empty;

                return;
            } else
            {
                Zprava_ukolu.Text = "Špatná vstupní data!";
            }
        }

        private void AddToDBProjectButton_Click(object sender, RoutedEventArgs e)
        {
            double pocet_dnu = 0;
            
            try
            {
                pocet_dnu = (Convertor.ConvertToDate(Datum_odevzdani.Text) - Convertor.ConvertToDate(Datum_vytvoreni.Text)).TotalDays;
            }
            catch { }

            if ((Nazev_projektu.Text.Length > 0 && Zadani_projektu.Text.Length > 0) && pocet_dnu >= 0)
            {
                try
                {
                    String sql = "INSERT INTO Projekty " + "(nazev, datum_vytvoreni, jmeno_klienta, zadani, technologie, datum_odevzdani, cas_vypracovani, pocet_komentare, pocet_bugy, pocet_problemy, id_problemy, id_bugy, hotovo, sazba, pocet_hodin, kos, datum_mazani, datum_uplneho_mazani) VALUES"
                            + "(@nazev, @datum_vytvoreni, @jmeno_klienta, @zadani, @technologie, @datum_odevzdani, @cas_vypracovani, @pocet_komentare, @pocet_bugy, @pocet_problemy, @id_problemy, @id_bugy, @hotovo, @sazba, @pocet_hodin, @kos, @datum_mazani, @datum_uplneho_mazani)";

                    string vystup_pocet_dnu = "";
                    
                    if (pocet_dnu > 1 && pocet_dnu < 5)
                    {
                        vystup_pocet_dnu += pocet_dnu;
                        vystup_pocet_dnu += " dny";
                    }
                    else if (pocet_dnu == 1)
                    {
                        vystup_pocet_dnu += pocet_dnu;
                        vystup_pocet_dnu += " den";
                    }
                    else if (pocet_dnu > 4)
                    {
                        vystup_pocet_dnu += pocet_dnu;
                        vystup_pocet_dnu += " dnů";
                    }

                    Dictionary<object, object> pole = new Dictionary<object, object>
                    {
                            {"@nazev", Nazev_projektu.Text},
                            {"@datum_vytvoreni", Datum_vytvoreni.Text},
                            {"@jmeno_klienta", Jmeno_klienta.Text},
                            {"@zadani", Zadani_projektu.Text},
                            {"@technologie", Technologie_vyvoje.Text},
                            {"@datum_odevzdani", Datum_odevzdani.Text},
                            {"@cas_vypracovani", vystup_pocet_dnu},
                            {"@pocet_komentare", 0},
                            {"@pocet_bugy", 0},
                            {"@pocet_problemy", 0},
                            {"@id_problemy", 0},
                            {"@id_bugy", 0},
                            {"@hotovo", 0},
                            {"@sazba", Sazba2.Text},
                            {"@pocet_hodin", 0},
                            {"@kos", false},
                            {"@datum_mazani", ""},
                            {"@datum_uplneho_mazani", ""}
                    };

                    if (DB.QueryVoid(pole, sql))
                    {
                        Zprava_ukolu.Text = "Přidán nový projekt!";
                    }
                }
                catch (Exception ex)
                {
                    System.Windows.MessageBox.Show(ex.Message);
                    return;
                }
                MenuItemClear();
                MenuItem1Button_Click(sender, e);

                tcSample.SelectedIndex = 0;
                Nazev_projektu.Text = string.Empty;
                Datum_vytvoreni.Text = string.Empty;
                Jmeno_klienta.Text = string.Empty;
                Zadani_projektu.Text = string.Empty;
                Technologie_vyvoje.Text = string.Empty;
                Datum_odevzdani.Text = string.Empty;
                Sazba2.Text = "";

                return;
            }
            else if (pocet_dnu < 0)
            {
                Zprava_ukolu.Text = "Špatně zadané datumy!";
            }
            else if (Nazev_projektu.Text.Length <= 0)
            {
                Zprava_ukolu.Text = "Nezadán žádný název!";
            }
            else if (Zadani_projektu.Text.Length <= 0)
            {
                Zprava_ukolu.Text = "Nezadáno zadání projektu!";
            }
        }

        #endregion AddToDB
    }
}