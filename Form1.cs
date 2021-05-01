using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Forms;
using Memory;
using System.IO;
using DiscordRPC;
using Newtonsoft.Json;

namespace cc55
{
    public partial class CC55 : Form
    {
        private Mem meme = new Mem();

        public static string logs = "";
        public static string Life = "ac_client.exe+0x00109B74,F8";
        public static string Shield = "ac_client.exe+0x00109B74,FC";
        public static string Grenad1 = "ac_client.exe+0x00109B74,158";
        public static string GunAmmo = "ac_client.exe+0x00109B74,13C";
        public static string SnipAmmo = "ac_client.exe+0x00109B74,14C";
        public static string RifleAmmo = "ac_client.exe+0x00109B74,150";
        public static string InfiniteClip2 = "ac_client.exe+0x00109B74,114";
        public static string InfiniteClip1 = "ac_client.exe+0x00109B74,128";
        public static bool errorToShow = false;
        public static bool logsnotcheck = true;
        public static bool CheatChecked = false;
        public static bool already_clicked_button1 = false;

        public CC55()
        {
            InitializeComponent();
        }

        public DiscordRpcClient client;

        private void InitDiscord()
        {
            DateTime now = DateTime.Now;

            client = new DiscordRpcClient("681252858058113030");

            client.OnReady += (sender, e) =>
            {
                Console.WriteLine("Received Ready from user {0}", e.User.Username);
            };

            client.OnPresenceUpdate += (sender, e) =>
            {
                Console.WriteLine("Received Update! {0}", e.Presence);
            };

            client.Initialize();

            client.SetPresence(new RichPresence()
            {
                Details = "Playing AssaultCube",
                State = "Cheating...",
                Assets = new Assets()
                {
                    LargeImageKey = "logo",
                    LargeImageText = "Using AssaultCube Cheat",
                }
            });
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            System.Media.SoundPlayer player = new System.Media.SoundPlayer(@"..\sounds\started.wav");
            player.Play();

            InitDiscord(); // Lancement -> Initialisation du discord rpc

            CheckForAssaCube(); // Lancement -> Vérification de lancement
        }

        // Fonction de vérification du lancement
        private void CheckForAssaCube()
        {
            int PID = meme.GetProcIdFromName("ac_client");
            if (PID <= 0)
            {
                this.errorLabel.Visible = true;
                this.errorLabel2.Visible = true;
                this.errorLabel3.Visible = true;
                this.errorLabel4.Visible = true;
                this.errorButton.Visible = true;

                this.errorLabel4.MouseHover += discord_MouseHover;

                this.riffleammo.Visible = false;
                this.godmodeshield.Visible = false;
                this.lifegodmode.Visible = false;
                this.guninfinitammo.Visible = false;
                this.sniperammo.Visible = false;
                this.infinitegrenade.Visible = false;
                this.clipriffle.Visible = false;
                this.gunclip.Visible = false;

                this.button1.Visible = false;
                this.pictureBox1.Visible = false;
                this.label1.Visible = false;

                if (errorToShow)
                {
                    MessageBoxButtons buttons = MessageBoxButtons.OK;
                    MessageBox.Show("Game not found, retry.", "Error", buttons, MessageBoxIcon.Error);
                }
                logs += "AssaultCube Cheat Started with error : AssaultCube not started";
                logsFunc();

                errorToShow = true;
            }
            else
            {
                this.errorLabel.Visible = false;
                this.errorLabel2.Visible = false;
                this.errorLabel3.Visible = false;
                this.errorLabel4.Visible = false;
                this.errorButton.Visible = false;

                this.riffleammo.Visible = true;
                this.godmodeshield.Visible = true;
                this.lifegodmode.Visible = true;
                this.guninfinitammo.Visible = true;
                this.sniperammo.Visible = true;
                this.infinitegrenade.Visible = true;
                this.clipriffle.Visible = true;
                this.gunclip.Visible = true;

                this.button1.Visible = true;
                this.pictureBox1.Visible = true;
                this.label1.Visible = true;

                logs += "AssaultCube Cheat Started without error.";
                logsFunc();
            }
        }

        // Fonction de message si souris au dessus
        private void discord_MouseHover(object sender, EventArgs e)
        {
            ToolTip tipsCopy = new ToolTip();
            tipsCopy.SetToolTip(this.errorLabel4, "Click to copy");
        }

        // Fonction sur click du bouton (si jeu lancé)
        private void button1_Click(object sender, EventArgs e)
        {
            if (!already_clicked_button1)
            {
                int PID = meme.GetProcIdFromName("ac_client");
                if (PID > 0)
                {
                    already_clicked_button1 = true;
                    meme.OpenProcess(PID);
                    if (riffleammo.Checked)
                    {
                        CheatChecked = true;
                        Thread AMMO = new Thread(writeAmmo) { IsBackground = true };
                        AMMO.Start();
                    }
                    if (lifegodmode.Checked)
                    {
                        CheatChecked = true;
                        Thread LIFE = new Thread(godmode) { IsBackground = true };
                        LIFE.Start();
                    }
                    if (godmodeshield.Checked)
                    {
                        CheatChecked = true;
                        Thread SHIELD = new Thread(shield) { IsBackground = true };
                        SHIELD.Start();
                    }
                    if (guninfinitammo.Checked)
                    {
                        CheatChecked = true;
                        Thread GUN = new Thread(gunAmmoinf) { IsBackground = true };
                        GUN.Start();
                    }
                    if (sniperammo.Checked)
                    {
                        CheatChecked = true;
                        Thread SNIP = new Thread(snipAmmoinf) { IsBackground = true };
                        SNIP.Start();
                    }
                    if (infinitegrenade.Checked)
                    {
                        CheatChecked = true;
                        Thread GRE_ONE = new Thread(Grenad1inf) { IsBackground = true };
                        GRE_ONE.Start();
                    }
                    if (clipriffle.Checked)
                    {
                        CheatChecked = true;
                        Thread IC_ONE = new Thread(IC1) { IsBackground = true };
                        IC_ONE.Start();
                    }
                    if (gunclip.Checked)
                    {
                        CheatChecked = true;
                        Thread IC_TWO = new Thread(IC2) { IsBackground = true };
                        IC_TWO.Start();
                    }
                }
                else
                {
                    logs += "ERROR : Game has been closed in the meantime.";
                    logsFunc();
                    MessageBoxButtons buttons = MessageBoxButtons.OK;
                    MessageBox.Show("You closed the game, restart it and try again.\nError?\n> Contact me on discord : ! Cuchi'#1632", "Error", buttons, MessageBoxIcon.Error);
                    already_clicked_button1 = false;
                    return;
                }

                if (!CheatChecked)
                {
                    logs += "ERROR : Nothing selected.";
                    logsFunc();
                    MessageBoxButtons buttons = MessageBoxButtons.OK;
                    MessageBox.Show("Nothing selected\nError?\n> Contact me on discord : ! Cuchi'#1632", "Error", buttons, MessageBoxIcon.Error);
                    already_clicked_button1 = false;
                    return;
                }
                else
                {
                    logs += "AssaultCube Cheat [ON] with :\n";

                    if (this.riffleammo.Checked)
                    {
                        CheatChecked = true;
                        logs += "   -> Infinite Riffle Ammo\n";
                    }
                    if (this.godmodeshield.Checked)
                    {
                        CheatChecked = true;
                        logs += "   -> Shield GodMode\n";
                    }
                    if (this.lifegodmode.Checked)
                    {
                        CheatChecked = true;
                        logs += "   -> Life GodMode\n";
                    }
                    if (this.guninfinitammo.Checked)
                    {
                        CheatChecked = true;
                        logs += "   -> Infinite Gun Ammo\n";
                    }
                    if (this.sniperammo.Checked)
                    {
                        CheatChecked = true;
                        logs += "   -> infinite Sniper Ammo\n";
                    }
                    if (this.infinitegrenade.Checked)
                    {
                        CheatChecked = true;
                        logs += "   -> Infinite Grenade\n";
                    }
                    if (this.clipriffle.Checked)
                    {
                        CheatChecked = true;
                        logs += "   -> Infinite Riffle Clip\n";
                    }
                    if (this.gunclip.Checked)
                    {
                        CheatChecked = true;
                        logs += "   -> Infinte Gun Clip\n";
                    }

                    System.Media.SoundPlayer player = new System.Media.SoundPlayer(@"..\sounds\start.wav");
                    player.Play();

                    this.label1.Text = "ON";
                    this.label1.ForeColor = System.Drawing.ColorTranslator.FromHtml("#97C98F");

                    this.riffleammo.Enabled = false;
                    this.godmodeshield.Enabled = false;
                    this.lifegodmode.Enabled = false;
                    this.guninfinitammo.Enabled = false;
                    this.sniperammo.Enabled = false;
                    this.infinitegrenade.Enabled = false;
                    this.clipriffle.Enabled = false;
                    this.gunclip.Enabled = false;

                    this.button1.Text = "STOP";
                }
                logsFunc();
            }
            else
            {
                System.Media.SoundPlayer player = new System.Media.SoundPlayer(@"..\sounds\stop.wav");
                player.Play();

                this.label1.Text = "OFF";
                this.label1.ForeColor = System.Drawing.ColorTranslator.FromHtml("#D4C296");

                this.riffleammo.Enabled = true;
                this.godmodeshield.Enabled = true;
                this.lifegodmode.Enabled = true;
                this.guninfinitammo.Enabled = true;
                this.sniperammo.Enabled = true;
                this.infinitegrenade.Enabled = true;
                this.clipriffle.Enabled = true;
                this.gunclip.Enabled = true;

                this.button1.Text = "START";
                already_clicked_button1 = false;
                CheatChecked = false;

                logs += "AssaultCube Cheat [OFF] | Reset of :\n";

                if (this.riffleammo.Checked)
                {
                    meme.WriteMemory(RifleAmmo, "int", "20");
                    logs += "   -> RifleAmmo : 20\n";
                }
                if (this.lifegodmode.Checked)
                {
                    meme.WriteMemory(Life, "int", "100");
                    logs += "   -> Life : 100\n";
                }
                if (this.godmodeshield.Checked)
                {
                    meme.WriteMemory(Shield, "int", "15");
                    logs += "   -> Shield : 15\n";
                }
                if (this.guninfinitammo.Checked)
                {
                    meme.WriteMemory(GunAmmo, "int", "10");
                    logs += "   -> GunAmmo : 10\n";
                }
                if (this.sniperammo.Checked)
                {
                    meme.WriteMemory(SnipAmmo, "int", "4");
                    logs += "   -> SnipAmmo : 4\n";
                }
                if (this.infinitegrenade.Checked)
                {
                    meme.WriteMemory(Grenad1, "int", "1");
                    logs += "   -> Grenad1 : 1\n";
                }
                if (this.clipriffle.Checked)
                {
                    meme.WriteMemory(InfiniteClip1, "int", "40");
                    logs += "   -> InfiniteClip1 : 40\n";
                }
                if (this.gunclip.Checked)
                {
                    meme.WriteMemory(InfiniteClip2, "int", "50");
                    logs += "   -> InfiniteClip2 : 50\n";
                }
                logsFunc();
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {
        }

        // Fonction munitions infinies sur la M4
        private void writeAmmo()
        {
            CheatChecked = true;
            while (already_clicked_button1)
            {
                if (riffleammo.Checked)
                {
                    meme.WriteMemory(RifleAmmo, "int", "666");
                    Thread.Sleep(2);
                }
                Thread.Sleep(2);
            }
        }

        // Fonction vie infinie
        private void godmode()
        {
            CheatChecked = true;
            while (already_clicked_button1)
            {
                if (lifegodmode.Checked)
                {
                    meme.WriteMemory(Life, "int", "666");
                    Thread.Sleep(1);
                }
                Thread.Sleep(2);
            }
        }

        // Fonction shield infinie
        private void shield()
        {
            CheatChecked = true;
            while (already_clicked_button1)
            {
                if (godmodeshield.Checked)
                {
                    meme.WriteMemory(Shield, "int", "666");
                    Thread.Sleep(1);
                }
                Thread.Sleep(2);
            }
        }

        // Fonction munitions infinies sur le pistolet
        private void gunAmmoinf()
        {
            CheatChecked = true;
            while (already_clicked_button1)
            {
                meme.WriteMemory(GunAmmo, "int", "666");
                Thread.Sleep(7);
            }
        }

        // Fonction munitions infinies sur le sniper
        private void snipAmmoinf()
        {
            CheatChecked = true;
            while (already_clicked_button1)
            {
                if (sniperammo.Checked)
                {
                    meme.WriteMemory(SnipAmmo, "int", "666");
                    Thread.Sleep(4);
                }
                Thread.Sleep(10);
            }
        }

        // Fonction munitions infinies pour la grenade
        private void Grenad1inf()
        {
            CheatChecked = true;
            while (already_clicked_button1)
            {
                if (infinitegrenade.Checked)
                {
                    meme.WriteMemory(Grenad1, "int", "6");
                    Thread.Sleep(4);
                }
                Thread.Sleep(10);
            }
        }

        // Fonction chargeurs infinies sur la M4
        private void IC1()
        {
            CheatChecked = true;
            while (already_clicked_button1)
            {
                if (clipriffle.Checked)
                {
                    meme.WriteMemory(InfiniteClip1, "int", "66");
                    Thread.Sleep(4);
                }
                Thread.Sleep(10);
            }
        }

        // Fonction chargeurs infinies sur le pistolet
        private void IC2()
        {
            CheatChecked = true;
            while (already_clicked_button1)
            {
                if (gunclip.Checked)
                {
                    meme.WriteMemory(InfiniteClip2, "int", "66");
                    Thread.Sleep(4);
                }
                Thread.Sleep(10);
            }
        }

        /*//////////////////////////////////////////*
        Full list des bruits sur click des checkboxs
        *///////////////////////////////////////////*

        /**////////////////////////////////////////////////////////////////////////////

        /**/

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            System.Media.SoundPlayer player = new System.Media.SoundPlayer(@"..\sounds\button.wav");
            player.Play();
            logs += "Riffle Infinite Ammo (un)checked";
            logsFunc();
        }

        /**/

        private void checkBox2_CheckedChanged_1(object sender, EventArgs e)
        {
            System.Media.SoundPlayer player = new System.Media.SoundPlayer(@"..\sounds\button.wav");
            player.Play();
            logs += "Shield GodMode (un)checked.";
            logsFunc();
        }

        /**/

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            System.Media.SoundPlayer player = new System.Media.SoundPlayer(@"..\sounds\button.wav");
            player.Play();
            logs += "Life GodMode (un)checked.";
            logsFunc();
        }

        /**/

        private void checkBox4_CheckedChanged(object sender, EventArgs e)
        {
            System.Media.SoundPlayer player = new System.Media.SoundPlayer(@"..\sounds\button.wav");
            player.Play();
            logs += "Gun Infinite Ammo (un)checked.";
            logsFunc();
        }

        /**/

        private void checkBox5_CheckedChanged(object sender, EventArgs e)
        {
            System.Media.SoundPlayer player = new System.Media.SoundPlayer(@"..\sounds\button.wav");
            player.Play();
            logs += "Sniper Infinite Ammo (un)checked.";
            logsFunc();
        }

        /**/

        private void checkBox6_CheckedChanged(object sender, EventArgs e)
        {
            System.Media.SoundPlayer player = new System.Media.SoundPlayer(@"..\sounds\button.wav");
            player.Play();
            logs += "Grenade Infinite (un)checked.";
            logsFunc();
        }

        /**/

        private void checkBox7_CheckedChanged(object sender, EventArgs e)
        {
            System.Media.SoundPlayer player = new System.Media.SoundPlayer(@"..\sounds\button.wav");
            player.Play();
            logs += "Riffle Infinite Clip (un)checked.";
            logsFunc();
        }

        /**/

        private void checkBox8_CheckedChanged(object sender, EventArgs e)
        {
            System.Media.SoundPlayer player = new System.Media.SoundPlayer(@"..\sounds\button.wav");
            player.Play();
            logs += "Gun Infinite Clip (un)checked.";
            logsFunc();
        }

        /**////////////////////////////////////////////////////////////////////////////

        // Easter Egg sound
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            System.Media.SoundPlayer player = new System.Media.SoundPlayer(@"..\sounds\easter.wav");
            player.Play();
        }

        // Fonction qui copie du texte
        private void label2_Click(object sender, EventArgs e)
        {
            Clipboard.SetText("! Cuchi'#1632");
        }

        // Fonction des logs
        private void logsFunc()
        {
            DateTime now = DateTime.Now;

            using (StreamWriter file = new StreamWriter(@"..\logs.txt", true))
            {
                var info = new FileInfo(@"..\logs.txt");
                if ((!info.Exists) || info.Length == 0)
                {
                    file.WriteLine("--------[ FIRST SESSION ]--------\n" + now.ToString("MM/dd/yyyy - HH:mm:ss") + " >> " + logs);
                    logs = "";
                    logsnotcheck = false;
                }
                if (logsnotcheck == true)
                {
                    file.WriteLine("--------[ END SESSION ]--------\n\n--------[ NEW SESSION ]--------\n" + now.ToString("MM/dd/yyyy - HH:mm:ss") + " >> " + logs);
                    logs = "";
                    logsnotcheck = false;
                }
                else
                {
                    file.WriteLine(now.ToString("MM/dd/yyyy - HH:mm:ss") + " >> " + logs);
                    logs = "";
                }
            }
        }

        // Fonction bouton rafraîchir (si jeu pas détécté)
        private void errorButton_Click(object sender, EventArgs e)
        {
            CheckForAssaCube();
        }
    }
}