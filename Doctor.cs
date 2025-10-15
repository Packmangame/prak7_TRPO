using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace prak7_romanov
{
    internal class Doctor: INotifyPropertyChanged
    {
        private long _id;
        public long Id {
            get => _id;
            set {
                if (_id != value)
                {
                    _id = value;
                    OnPropertyChanged();
                }
            }
        }

        string _name = "Адольф";
        public string Name { get=>_name;
            set {
                if (_name != value)
                {
                    _name = value;
                    OnPropertyChanged();
                }
            } 
        }

        string _lastname = "Генеральевич";
        public string LastName { get=>_lastname; 
            set{
                if (_lastname != value)
                {
                    _lastname = value;
                    OnPropertyChanged();
                }
            }
        }

        string _middlename = "Адольфович";
        public string MiddleName { get=>_middlename; set
            {
                if (_middlename != value)
                {
                    _middlename = value;
                    OnPropertyChanged();
                }
            }
        }
        
        string _specialisation = "Окулист";
        public string Specialisation { get=> _specialisation; set
            {
                if (_specialisation != value)
                {
                    _specialisation = value;
                    OnPropertyChanged();
                }
            }
        }

        string _password;
        public string Password
        {
            get => _password;
            set
            {
                if (_password != value)
                {
                    _password = value;
                    OnPropertyChanged();
                }
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string? propName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }

    }
}
