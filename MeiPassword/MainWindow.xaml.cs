﻿using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Input;
using MeiPassword.Algorythmen;
using MeiPassword.ConfigsSystem;
using ModernMessageBoxLib;

namespace MeiPassword
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
       public bool priv { get; set; }
        public bool priv2 { get; set; }
        private readonly string pw = "31xZpgRO#5pC1c-cI{-EOj%sCmz9OtUx9fryUuGPCGxqD%#dnlK1%xAs2fwcMsaMHYcuREnwvnbhRNLEnwvDF3zlC%+P%DctGIGL{KrF-wfQv28K1PUz-4gn9XPSI31xZpgRO#5pC1c-cI{-EOj%sCmz9OtUx9fryUuGPCGxqD%#dnlK1%xAs2fwcMsaMHYcuREnwvnbhRNLEnwvDF3zlC%+P%DctGIGL{KrF-wfQv28K1PUz-4gn9XPSI31xZpgRO#5pC1c-cI{-EOj%sCmz9OtUx9fryUuGPCGxqD%#dnlK1%xAs2fwcMsaMHYcuREnwvnbhRNLEnwvDF3zlC%+P%DctGIGL{KrF-wfQv28K1PUz-4gn9XPSIie";
        public MainWindow()
        {
            var MyIni = new IniFile(PathFinding.CONFIGFILE);
            ConfigsSystem.Check_Start.Check();

            InitializeComponent();
            this.MouseLeftButtonDown += delegate { DragMove(); };
            string privatecy = MyIni.Read("PrivatePolicy", "System");
            priv = Boolean.Parse(privatecy);
            string music = MyIni.Read("Music", "Audio");
            string Discordss = MyIni.Read("DiscordRPC", "System").ToString();
            string AutoLogin = MyIni.Read("AutoLogin", "System").ToString();
            bool Discord = Boolean.Parse(Discordss);
            bool AutoLogins = Boolean.Parse(AutoLogin);
            priv2 = Boolean.Parse(music);
            if (!priv)
            {
                var msg = new ModernMessageBox("Diese Programm Speichert und Verschlüsselt ihre Passwörter.\n Wenn die Master Passwörter Verloren gehen gibt es keine Möglichkeiten die Passwörter wiederherzustellen\n", "Azusa Passwort Meneger Massege", ModernMessageboxIcons.Info, "Okay")
                {
                    Button1Key = Key.D1,
                    CheckboxText = "Ich habe Verstanden",
                    CheckboxVisibility = Visibility.Visible,

                };
                msg.ShowDialog();
                if(msg.CheckboxChecked == true)
                {
                    MyIni.Write("PrivatePolicy", "true", "System");
                }
                else
                {
                    System.Environment.Exit(1);
                }
            }

            if (priv2)
            {
                BackgroundSysteme.MusicSysteme.play(true);
               
            }
            if (Discord)
            {
                DiscordRPC.Discord_RPC.rpc(true, false, false, false, false);
            }
            if (AutoLogins)
            {
                Algorythmen.Auth_Class_System.password_crypt = MeisXOR.XORConverter.MeiXORDecrypt(MyIni.Read("PSW2", "PasswortFileSystem").ToString(), pw);
                Algorythmen.Auth_Class_System.salt_key = MeisXOR.XORConverter.MeiXORDecrypt(MyIni.Read("PIN", "PasswortFileSystem").ToString(), pw);
                UI_Management.ControlScreen data = new UI_Management.ControlScreen();
                data.Show();
                this.Close();
            }
            rename();

        }

        private void Login_Button_Click(object sender, RoutedEventArgs e)
        {
            /*
            UI_Management.ControlScreen data = new UI_Management.ControlScreen();
            data.Show();
            this.Close();
            */
            
            bool checker = false;

            if (PasswordCrypter.Password.Length > 15)
            {
                Algorythmen.Auth_Class_System.password_crypt = PasswordCrypter.Password;
            }
            if (Secure_key.Password != "")
            {
                Algorythmen.Auth_Class_System.salt_key = Secure_key.Password;
            } 
            if (Secure_key.Password == "" && PasswordCrypter.Password == "")
            {
                checker = false;

                QModernMessageBox.Show("Please fill out everything+", "Application Error", QModernMessageBox.QModernMessageBoxButtons.Ok, ModernMessageboxIcons.Info);
                return;
            } else
            {
                checker = true;
            }


            if (checker)
            {
                SChecked(Algorythmen.Auth_Class_System.password_crypt, Algorythmen.Auth_Class_System.salt_key);
                UI_Management.ControlScreen data = new UI_Management.ControlScreen();
                data.Show();
                this.Hide();
            }
             

        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            DiscordRPC.Discord_RPC.rpc(false, false, false, true, false);
            System.Environment.Exit(1);
        }

        private void Minimize_Button_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void SChecked(string crypt, string pin)
        {
            var MyIni = new IniFile(PathFinding.CONFIGFILE);
            if (Speicher_Es.IsChecked == true)
            {
                MyIni.Write("PIN", MeisXOR.XORConverter.MeiXOREncrypt(pin, pw), "PasswortFileSystem");
                MyIni.Write("PSW2", MeisXOR.XORConverter.MeiXOREncrypt(crypt, pw), "PasswortFileSystem");
                MyIni.Write("AutoLogin", "true", "System");
                return;
            }

            if (Speicher_Es.IsChecked == false)
            {
                
                return;
            }
        }



        public void rename()
        {
            int data = Check_Start.checkvaleu();
            if (data == 1) englisch();
            if (data == 0) german();
        }

        void englisch()
        {
            titel_wel.Content = "WELCOME";
            des1.Content = "Please enter your used password to decrypt your passwords";
            des2.Content = "or specify a new one to save some";
            pw_text.Content = "Password";
            Speicher_Es.Content = "Autologin (Uncertain, not recommended)";
        }

        void german()
        {
            titel_wel.Content = "Willkommen";
            des1.Content = "Bitte gebe dein verwendetes Passwort um deine Passwörter zu entschlüsseln";
            des2.Content = "oder gebe ein neues an um welche  zu Speichern";
            pw_text.Content = "Passwort";
            Speicher_Es.Content = "Autologin (Unsicher, nicht empfohlen)";
        }
    }
}