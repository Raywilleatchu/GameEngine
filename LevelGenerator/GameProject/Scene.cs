using LevelGenerator.GameProject.Components;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace LevelGenerator.GameProject
{
    [DataContract]
    public class Scene : ViewModelBase
    {
        private string _name;
        [DataMember]
        public string Name
        {
            get { return _name; }
            set 
            { 
                if(_name != value)
                    _name = value; 
            }
        }

        [DataMember]
        public Project Project { get; private set; }

        private bool _isActive;
        [DataMember]
        public bool IsActive
        {
            get => _isActive;

            set
            {
                if(_isActive != value)
                {
                    _isActive = value;
                    OnPropertyChanged(nameof(IsActive));
                }

            }

        }

        [DataMember(Name = nameof(GameEntities))]
        private readonly ObservableCollection<GameEntity> gameEntities = new ObservableCollection<GameEntity>();
        public ReadOnlyCollection<GameEntity> GameEntities { get; }

        public Scene (Project project, string name)
        {
            Debug.Assert(project != null);
            Project = project;
            Name = name;
        }
    }
}
