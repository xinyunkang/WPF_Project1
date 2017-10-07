using FriendOrganizer.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Threading.Tasks;


namespace FriendOrganizer.UI.Wrapper
{
    public partial class FriendWrapper: NotifyDataErrorInfoBase
    {
        public FriendWrapper(Friend model)
        {
            Model = model;
        }

        public Friend Model { get;  }

        public int Id { get { return Model.Id; } }

       
        public string FirstName
        {
            get { return Model.FirstName; }
            set {
                Model.FirstName = value;
                OnPropertyChanged();
                ValidateProperty(nameof(FirstName),value);
            }
        }

        private void ValidateProperty(string propertyName, object currentValue)
        {
            ClearErrors(propertyName);
            //validate annotation 
            ValidateAnnotation(propertyName, currentValue);
            //validate custom errors.
            ValidateCustomError(propertyName);
        }

        private void ValidateCustomError(string propertyName)
        {
            switch (propertyName)
            {
                case nameof(FirstName):
                    if (string.Equals(FirstName, "Robot", StringComparison.OrdinalIgnoreCase))
                    {
                        AddError(propertyName, "Robot is not valid friend");
                    }
                    break;
            }
        }

        private void ValidateAnnotation(string propertyName, object currentValue)
        {
            var results = new List<ValidationResult>();
            var context = new ValidationContext(Model) { MemberName = propertyName };
            Validator.TryValidateProperty(currentValue, context, results);
            foreach (var result in results)
            {
                AddError(propertyName, result.ErrorMessage);
            }
        }

        public string LastName
        {
            get { return Model.LastName; }
            set
            {
                Model.LastName = value;
                OnPropertyChanged();
                ValidateProperty(nameof(LastName), value);
            }
        }

        public string Email
        {
            get { return Model.Email; }
            set
            {
                Model.Email = value;
                OnPropertyChanged();
                ValidateProperty(nameof(Email), value);
            }
        }

        public int? FavoriteLanguageId
        {
            get { return Model.FavoriteLanguageId; }
            set
            {
                Model.FavoriteLanguageId = value;
                OnPropertyChanged();              
            }
        }
    }
}
