using LevelGenerator.GameProject;
using LevelGenerator.GameProject.Components;
using LevelGenerator.GameProject.Utilities;
using System;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Input;

namespace LevelGenerator.Editors
{
    /// <summary>
    /// Interaction logic for GameEntityView.xaml
    /// </summary>
    public partial class GameEntityView : UserControl
    {
        private Action _undoAction;
        private Action _redoAction;
        private Action _isEnabled;
        private string _propertyName;
        public static GameEntityView Instance { get; private set; }
        public GameEntityView()
        {
            InitializeComponent();
            DataContext = null;
            Instance = this;
            DataContextChanged += (_, __) =>
            {
                if (DataContext != null)
                {
                    (DataContext as MSEntity).PropertyChanged += (s, e) => _propertyName = e.PropertyName;
                }
            };
        }

        private Action GetRenameAction()
        {
            var vm = DataContext as MSEntity;
            var selection = vm.SelectedEntities.Select(x => (x, x.Name)).ToList();
            return new Action(() =>
            {
                selection.ForEach(item => item.x.Name = item.Name);
                (DataContext as MSEntity).Refresh();
            });
        }

        private Action GetIsEnabledAction()
        {
            var vm = DataContext as MSEntity;
            var selection = vm.SelectedEntities.Select(x => (x, x.IsEnabled)).ToList();
            return new Action(() =>
            {
                selection.ForEach(item => item.x.IsEnabled = item.IsEnabled);
                (DataContext as MSEntity).Refresh();
            });
        }

        private void OnName_TextBox_GotKeyBoardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            _undoAction = GetRenameAction();
        }

        private void OnName_TextBox_LostKeyBoardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            if (_propertyName == nameof(MSEntity.Name) && _undoAction != null)
            {
                _redoAction = GetRenameAction();
                Project.UndoRedo.Add(new UndoRedoAction(_undoAction, _redoAction, "Rename Game Entity"));
                _propertyName = null;
            }
            _undoAction = null;
        }

        private void OnIsEnabled_CheckBox_Click(object sender, System.Windows.RoutedEventArgs e)
        { 
            var undoAction = GetIsEnabledAction();
            var vm = DataContext as MSEntity;
            vm.IsEnabled = (sender as CheckBox).IsChecked == true;
            var redoAction = GetIsEnabledAction();
            Project.UndoRedo.Add(new UndoRedoAction(undoAction, redoAction, vm.IsEnabled == true ? "Game Enable Entity" : "Game Disable Entity"));
        }
    }
}
