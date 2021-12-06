using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parser.Entities
{
    public class CyberDangerInfo : IEquatable<CyberDangerInfo>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string Source { get; set; }

        public string Target { get; set; }

        public bool ConfidentialityViolation { get; set; }

        public bool IntegrityViolation { get; set; }

        public bool AvailabilityViolation { get; set; }

        public static Builder BuildNew() => new Builder();

        public override string ToString()
        {
            return $"УБИ.{Id} - {Name}";
        }

        public class Builder
        {
            private int _id;
            private string _name;
            private string _description;
            private string _source;
            private string _target;
            private bool _confidentialityViolation;
            private bool _integrityViolation;
            private bool _availabilityViolation;

            public Builder Id(int id)
            {
                _id = id;
                return this;
            }

            public Builder Name(string name)
            {
                _name = name;
                return this;
            }

            public Builder Description(string description)
            {
                _description = description;
                return this;
            }

            public Builder Source(string source)
            {
                _source = source;
                return this;
            }

            public Builder Target(string target)
            {
                _target = target;
                return this;
            }

            public Builder ConfidentialityViolation(bool state)
            {
                _confidentialityViolation = state;
                return this;
            }

            public Builder IntegrityViolation(bool state)
            {
                _integrityViolation = state;
                return this;
            }

            public Builder AvaliabilityViolation(bool state)
            {
                _availabilityViolation = state;
                return this;
            }

            public CyberDangerInfo Build()
            {
                return new CyberDangerInfo
                {
                    Id = _id,
                    Name = _name,
                    Description = _description,
                    Source = _source,
                    Target = _target,
                    ConfidentialityViolation = _confidentialityViolation,
                    IntegrityViolation = _integrityViolation,
                    AvailabilityViolation = _availabilityViolation,
                };
            }

        }

        public bool Equals(CyberDangerInfo other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Id == other.Id && 
                   Name == other.Name && 
                   Description == other.Description && 
                   Source == other.Source && 
                   Target == other.Target && 
                   ConfidentialityViolation == other.ConfidentialityViolation && 
                   IntegrityViolation == other.IntegrityViolation && 
                   AvailabilityViolation == other.AvailabilityViolation;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((CyberDangerInfo) obj);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id, Name, Description, Source, Target, ConfidentialityViolation, IntegrityViolation, AvailabilityViolation);
        }
    }
}
