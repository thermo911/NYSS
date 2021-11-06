using System;
using System.IO;
using Notebook.Validators;

namespace Notebook.Entities
{
    class Note : IEquatable<Note>
    {
        private static int _count;
        private static IStringValidator _nameValidator;
        private static IStringValidator _surnameValidator;
        private static IStringValidator _middleNameValidator;
        private static IStringValidator _phoneValidator;
        private static IStringValidator _countryValidator;
        private static IStringValidator _positionValidator;
        private static IStringValidator _companyValidator;
        private static IStringValidator _dateValidator;

        private int _id;
        private string _name;
        private string _surname;
        private string _middleName;
        private string _phone;
        private string _country;
        private string _position;
        private string _company;
        private string _birthday;
        private string _other;

        static Note()
        {
            _nameValidator = new SingleTitleValidator();
            _surnameValidator = new SingleTitleValidator();
            _middleNameValidator = new SingleTitleValidator();
            _phoneValidator = new PhoneNumberValidator();
            _countryValidator = new MultiTitleValidator();
            _positionValidator = new MultiTitleValidator();
            _companyValidator = new MultiTitleValidator();
            _dateValidator = new DateValidator();
        }
        public Note(string name, string surname, string phone, string country)
        {
            _id = ++_count;
            Name = name;
            Surname = surname;
            Phone = phone;
            Country = country;
        }

        public int Id => _id;

        public string Name
        {
            get => _name;
            set
            {
                if (!_nameValidator.Validate(value))
                    throw new InvalidDataException("Name - single word contains of letters");
                _name = value;
            }
        }

        public string Surname
        {
            get => _surname;
            set
            {
                if (!_surnameValidator.Validate(value))
                    throw new InvalidDataException("Surname - single word contains of letters");
                _surname = value;
            }
        }

        public string MiddleName
        {
            get => _middleName;
            set
            {
                if (string.IsNullOrWhiteSpace(value)) _middleName = "<unknown>";
                else if (_middleNameValidator.Validate(value))
                    _middleName = value;
                else throw new InvalidDataException("Middle name - single word contains of letters");
            }
        }

        public string Phone
        {
            get => _phone;
            set
            {
                if (!_phoneValidator.Validate(value))
                    throw new InvalidDataException("Phone format: (+)########### (11 digits)");
                _phone = value;
            }
        }

        public string Country
        {
            get => _country;
            set
            {
                if (!_countryValidator.Validate(value))
                    throw new InvalidDataException("Country - group of words divided by space or '-'");
                _country = value;
            }
        }

        public string Birthday { 
            get => _birthday;
            set
            {
                if (string.IsNullOrWhiteSpace(value)) _birthday = "<unknown>";
                else if (_dateValidator.Validate(value))
                    _birthday = value;
                else throw new InvalidDataException("Date format: DD.MM.YYYY or DD-MM-YYYY or DD/MM/YYYY");
            }
        }

        public string Company
        {
            get => _company;
            set
            {
                if (string.IsNullOrWhiteSpace(value)) _company = "<unknown>";
                else if (_companyValidator.Validate(value))
                    _company = value;
                else throw new InvalidDataException("Company - group of words divided by space or '-'");
            }
        }

        public string Position
        {
            get => _position;
            set
            {
                if (string.IsNullOrWhiteSpace(value)) _position = "<unknown>";
                else if (_positionValidator.Validate(value))
                    _position = value;
                else throw new InvalidDataException("Position - group of words divided by space or '-'");
            }
        }
        public string Other
        {
            get => _other;
            set => _other = value;
        }

        public override string ToString() => $"#{_id} : {Name} {Surname} - {_phone}";

        public bool Equals(Note other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return _id == other._id;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Note) obj);
        }

        public override int GetHashCode() => _id;
    }
}
