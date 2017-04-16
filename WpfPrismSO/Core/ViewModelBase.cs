using Prism;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using WpfPrismSO.Core;
using WpfPrismSO.Events;

namespace WpfPrismSO
{
    public class ViewModelBase : BindableBase, INotifyDataErrorInfo, INavigationAware //, IActiveAware
    {
        private string _messageToDisplay = "Ready";
        private bool _isMessageVisible = false;
        protected Dictionary<string, List<string>> _errors;
        private bool _isbusy;
        private readonly IEventAggregator _eventAggregator;

        //protected QIQOBusinessService.UserSession _session;

        protected ViewModelBase(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
            _errors = new Dictionary<string, List<string>>();

            Process currentProcess = Process.GetCurrentProcess();
            ProcessID = currentProcess.Id;

            HostName = Environment.MachineName;
            DamainName = Environment.UserDomainName;
            UserName = Environment.UserName;

            BoundCommandBase = new DelegateCommand(DoWorkBase);
        }

        protected virtual void DoWorkBase()
        {
            _eventAggregator.GetEvent<PollingEvent>().Publish($"From Base Class");
        }

        public DelegateCommand BoundCommandBase { get; set; }

        public string MessageToDisplay
        {
            get { return _messageToDisplay; }
            set
            {
                _messageToDisplay = value;
                RaisePropertyChanged();
            }
        }
        public bool IsMessageVisible
        {
            get { return _isMessageVisible; }
            set
            {
                _isMessageVisible = value;
                RaisePropertyChanged();
            }
        }
        public bool IsBusy
        {
            get { return _isbusy; }
            set
            {
                _isbusy = value;
                RaisePropertyChanged();
            }
        }

        public object CurrentCompany { get; }
        public string CurrentCompanyName { get; }
        public int CurrentCompanyKey { get; }
        public int ProcessID { get; }
        public string DamainName { get; }
        public string UserName { get; }

        public string HostName { get; }

        public virtual string ViewTitle { get; protected set; } // => string.Empty;

        public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;
        // public virtual event EventHandler IsActiveChanged;
        public bool IsActive { get; set; }

        protected void SetErrors(string propertyName, List<string> propertyErrors)
        {
            _errors.Remove(propertyName);
            _errors.Add(propertyName, propertyErrors);
            ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(propertyName));
        }
        protected void ClearErrors(string propertyName)
        {
            _errors.Remove(propertyName);
            ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(propertyName));
        }

        public System.Collections.IEnumerable GetErrors(string propertyName)
        {
            if (string.IsNullOrEmpty(propertyName))
            {
                return (_errors.Values);
            }
            else
            {
                if (_errors.ContainsKey(propertyName))
                {
                    return (_errors[propertyName]);
                }
                else
                {
                    return null;
                }
            }
        }

        public virtual void OnNavigatedTo(NavigationContext navigationContext) { }
        public virtual bool IsNavigationTarget(NavigationContext navigationContext) { return true; }
        public virtual void OnNavigatedFrom(NavigationContext navigationContext) { }
        protected virtual void DisplayErrorMessage(Exception ex, [CallerMemberName] string propertyName = "") { }

        public bool HasErrors => (_errors.Count > 0);

        //protected T ExecuteFaultHandledOperation<T>(Func<T> codetoExecute)
        //{
        //    try
        //    {
        //        return codetoExecute.Invoke();
        //    }
        //    catch (FaultException ex)
        //    {
        //        Log.Error(ex.Source + " : " + ex.Message);
        //        DisplayErrorMessage(ex);
        //    }
        //    catch (Exception ex)
        //    {
        //        Log.Error(ex.Source + " : " + ex.Message);
        //        DisplayErrorMessage(ex);
        //    }
        //}

        protected void ExecuteFaultHandledOperation(Action codetoExecute)
        {
            IsBusy = true;
            try
            {
                codetoExecute.Invoke();
            }
            //catch (FaultException ex)
            //{
            //    Log.Error("FaultException from:" + ex.Source + " : " + ex.Message);
            //    Log.Error(ex, ex.Message);
            //    DisplayErrorMessage(ex);
            //}
            //catch (CommunicationException ex)
            //{
            //    Log.Error("CommunicationException from: " + ex.Source + " : " + ex.Message);
            //    Log.Error(ex, ex.Message);
            //    if (ex.InnerException != null) Log.Error(ex.InnerException, "Inner Exception");
            //    DisplayErrorMessage(ex);
            //}
            catch (TimeoutException ex)
            {
                Log.Error("TimeoutException from: " + ex.Source + " : " + ex.Message);
                Log.Error(ex.Message);
                DisplayErrorMessage(ex);
            }
            catch (Exception ex)
            {
                DisplayErrorMessage(ex);
                Log.Error("Generic Exception from:" + ex.Source + " : " + ex.Message);
                Log.Error(ex.Message);
                //Log.Error(ex.InnerException, "Inner Exception***:" + ex.InnerException.Message);
            }
            finally
            {
                IsBusy = false;
            }
        }
    }

}
