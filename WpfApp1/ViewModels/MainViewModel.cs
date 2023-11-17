using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using WpfApp1.Commands;

namespace WpfApp1.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        #region PRIVATE VARIABLES
        private string _tbValue;
        private List<string> _avalibleOperations = new() {"+","-","*","/" };
        private DataTable _dataTable = new DataTable();
        #endregion
        public event PropertyChangedEventHandler? PropertyChanged;

        public ICommand AddNumberCommand { get; set; }

        public ICommand AddOperationCommand { get; set; }

        public ICommand GetResultCommand {  get; set; }

        public ICommand ClearScreenCommand { get; set; }

        public MainViewModel()
        {
            TbVal = "0";
            AddNumberCommand = new RelayCommand(AddNumber);
            AddOperationCommand = new RelayCommand(AddOperation);
            GetResultCommand = new RelayCommand(GetResultOperation);
            ClearScreenCommand = new RelayCommand(ClearScreenOperation);
        }

        private void ClearScreenOperation(object obj)
        {
            TbVal = "0";
        }

        private void GetResultOperation(object obj)
        {
            if (IsSignLast())
                _tbValue += "0";
            var res = _dataTable.Compute(TbVal, "");
            TbVal = res.ToString(); ;
        }

        private void AddOperation(object obj)
        {
            if (_avalibleOperations.Contains(TbVal.Substring(TbVal.Length - 1)))
                return;

            TbVal += obj as string;
        }

        private void AddNumber(object obj)
        {

            var num = obj as string;
            
            if (TbVal == "0" && num != ".")
            {
                TbVal = string.Empty;
            }
            else if (num == "." && _avalibleOperations.Contains(TbVal.Substring(TbVal.Length-1)))
            {
                num = "0.";
            }

            TbVal += num;

        }

        

        public string TbVal
        {
            get { return _tbValue; }
            set
            { 
                _tbValue = value;
                OnPropertyChanged();
            }
        }

        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private bool IsSignLast() {
            return _avalibleOperations.Contains(TbVal.Substring(TbVal.Length - 1));
        }
    }
}
