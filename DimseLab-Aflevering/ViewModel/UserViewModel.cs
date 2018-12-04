﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using DimseLab_Aflevering.Annotations;
using DimseLab_Aflevering.Model;
using GalaSoft.MvvmLight.Command;

namespace DimseLab_Aflevering.ViewModel
{
    class UserViewModel : INotifyPropertyChanged
    {
        // Opretter reference til ModelController
        private ModelController _mC = ModelController.Instance;

        // Opretter instanser af bruger oplysninger - bruges til visning af nuværende bruger
        private String _firstName = ModelController.Instance.CurrentUser.FirstName;
        private String _lastName = ModelController.Instance.CurrentUser.LastName;
        private String _email = ModelController.Instance.CurrentUser.Email;

        /// <summary>
        /// Constructor
        /// </summary>
        public UserViewModel()
        {
            // Knap til visning af userGrid
            UserProfileButton = new RelayCommand(OpenUserProfile);
        }

        /// <summary>
        /// Metode til visning af userGrid
        /// </summary>
        private void OpenUserProfile()
        {
            ModelController.Instance.SetAllInvisible();
            ModelController.Instance.MyProfileVisibility = true;
        }

        /*login system:
        
        public sealed partial class MainPage
        {
            public MainPage()
            {
                this.InitializeComponent();
            }

            private void Button_Click(object sender, RoutedEventArgs e)
            {
                //txtUsername og txtPassword bindes til en TextBox og PasswordBox
                string username, password;
                username = txtUsername.Text;
                password = txtPassword.Password;
                if (username == "admin@easj.dk" && password == "1234")
                {
                    // bruger vises nu som "logged in" og man vises videre til startsiden
                    //Debug.Write("Success");
                }
                else
                {
                    // mangler evt. en pop-up message med "error"
                    //Debug.Write("Error");
                }
            }
        }*/

        #region Properties

        public RelayCommand UserProfileButton { get; set; }

        public string FirstName
        {
            get { return _firstName; }
            set
            {
                _firstName = value;
                OnPropertyChanged();
            }
        }

        public string LastName
        {
            get { return _lastName; }
            set { _lastName = value; }
        }

        public string Email
        {
            get { return _email; }
            set { _email = value; }
        }

        public ModelController MC
        {
            get { return _mC; }
            set { _mC = value; }
        }

        #endregion

        #region INotifyProperty

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion

    }
}
