﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
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
using MeiPassword.Algorythmen;
using MeiPassword.ConfigsSystem;
using ModernMessageBoxLib;

namespace MeiPassword.UI_Management
{
    /// <summary>
    /// Interaktionslogik für Settings.xaml
    /// </summary>
    public partial class Settings : Window
    {

        private readonly string pw = "passwrorrdd";
        public Settings()
        {

            InitializeComponent();
            DiscordRPC.Discord_RPC.rpc(false, true, false, false);
            var MyIni = new IniFile(PathFinding.CONFIGFILE);
            string edsd = MyIni.Read("DiscordRPC", "System");
            string AutoLogin = MyIni.Read("AutoLogin", "System").ToString();
            string musics = MyIni.Read("Music", "Audio");
            bool check = Boolean.Parse(AutoLogin);
            bool music = Boolean.Parse(musics);
            bool Discord = Boolean.Parse(edsd);
            MusicSelection(music);
            DiscordRPCCheck(Discord);
            AutoLoginCheck(check);
            this.MouseLeftButtonDown += delegate { DragMove(); };
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            DiscordRPC.Discord_RPC.rpc(false, false, true, false);
            this.Close();

        }

        private void OpenPasswordFolder_Click(object sender, RoutedEventArgs e)
        {
            Process.Start(PathFinding.PASSWORDFOLDER);
        }

        private void OpenConfig_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Process.Start("notepad.exe", PathFinding.CONFIGFILE);
            } catch(Exception x)
            {
                QModernMessageBox.Show($"Ein Böses Errorlein: \n{x}", "Application Error", QModernMessageBox.QModernMessageBoxButtons.Ok, ModernMessageboxIcons.Info);
            }
         
        }

        private void OpenFolder_Click(object sender, RoutedEventArgs e)
        {
            Process.Start(PathFinding.MAIN);
        }

        private void Music_OnOff_Checked(object sender, RoutedEventArgs e)
        {
            bool check = (bool)Music_OnOff.IsChecked;
            if (!check)
            {
                DisableAudio();
                return;
            }
            if (check)
            {
                EnableAudio();
                return;
            }
        }

        private void Disable_Enable_Discord_Checked(object sender, RoutedEventArgs e)
        {
            var MyIni = new IniFile(PathFinding.CONFIGFILE);
           
            if (Disable_Enable_Discord.IsChecked == true)
            {
                string edsd = MyIni.Read("DiscordRPC", "System");
                bool Discord = Boolean.Parse(edsd);
                if (Discord == false)
                {
                    MyIni.Write("DiscordRPC", "true", "System");
                    Disable_Enable_Discord.IsChecked = true;
                }
            } else
            {
                string edsd = MyIni.Read("DiscordRPC", "System");
                bool Discord = Boolean.Parse(edsd);
                if (Discord == true)
                {
                    MyIni.Write("DiscordRPC", "false", "System");
                    Disable_Enable_Discord.IsChecked = false;
                }
               
            }
        }

        private void AutoLogin_Checked(object sender, RoutedEventArgs e)
        {
            var MyIni = new IniFile(PathFinding.CONFIGFILE);
            bool check = (bool)AutoLogin.IsChecked;
            if (check)
            {
                MyIni.DeleteKey("AutoLogin", "System");
                MyIni.Write("PIN", MeisXOR.XORConverter.MeiXOREncrypt(Auth_Class_System.salt_key, pw), "PasswortFileSystem");
                MyIni.Write("PSW1", MeisXOR.XORConverter.MeiXOREncrypt(Auth_Class_System.password_xor, pw), "PasswortFileSystem");
                MyIni.Write("PSW2", MeisXOR.XORConverter.MeiXOREncrypt(Auth_Class_System.password_crypt, pw), "PasswortFileSystem");
                MyIni.Write("AutoLogin", "true", "System");
            }
            if (!check)
            {
                MyIni.DeleteKey("AutoLogin", "System");
                MyIni.Write("PIN", "", "PasswortFileSystem");
                MyIni.Write("PSW1", "", "PasswortFileSystem");
                MyIni.Write("PSW2", "", "PasswortFileSystem");
                MyIni.Write("AutoLogin", "false", "System");
            }
        }









        private void DiscordRPCCheck(bool discord)
        {
            if (discord == false) Disable_Enable_Discord.IsChecked = false;
            if (discord == true)  Disable_Enable_Discord.IsChecked = true;
        }
        private void MusicSelection(bool b)
        {
            if (b == false)
            {
                Music_OnOff.IsChecked = false;
                return;
            }
            if (b == true)
            {
                Music_OnOff.IsChecked = true;
                return;
            }
        }
        private void AutoLoginCheck(bool music)
        {
            if (music == false)
            {
                AutoLogin.IsChecked = false;
                return;
            }

            if (music == true)
            {
                AutoLogin.IsChecked = true;
                return;
            }
        }

        private void EnableAudio()
        {
            var MyIni = new IniFile(PathFinding.CONFIGFILE);
            bool b = Boolean.Parse(MyIni.Read("Music", "Audio").ToString());
            if (!b)
            {
                try
                {
                    MyIni.DeleteKey("Music", "Audio");
                    MyIni.Write("Music", "true", "Audio");
                    BackgroundSysteme.MusicSysteme.play(true);
                }
                catch (Exception x)
                {
                    QModernMessageBox.Show($"Ein Böses Errorlein: \n{x}", "Application Error", QModernMessageBox.QModernMessageBoxButtons.Ok, ModernMessageboxIcons.Info);
                }

            }
            return;
        }
        private void DisableAudio()
        {
          
            var MyIni = new IniFile(PathFinding.CONFIGFILE);
            bool b = Boolean.Parse(MyIni.Read("Music", "Audio").ToString());
            if (b)
            {
                try
                {
                    MyIni.DeleteKey("Music", "Audio");
                    MyIni.Write("Music", "false", "Audio");
                    BackgroundSysteme.MusicSysteme.play(false);
                }
                catch (Exception x)
                {
                    QModernMessageBox.Show($"Ein Böses Errorlein: \n{x}", "Application Error", QModernMessageBox.QModernMessageBoxButtons.Ok, ModernMessageboxIcons.Info);
                }
             
            }
            return;
        }

        private void RGB_Color_Checked(object sender, RoutedEventArgs e)
        {

        }
    }
}
