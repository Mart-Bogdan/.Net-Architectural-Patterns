using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Windows;
using System.Windows.Input;
using JetBrains.Annotations;
using WorkWithDB.DAL.Abstract;
using WorkWithDB.Entity;
using WorkWithDB.Entity.Entities;
using WorkWithDB.UI.MVVM;

namespace WorkWithDB.UI.ViewModels
{
    public class EditPostVM : INotifyPropertyChanged, IDataErrorInfo
    {
        private readonly BlogPost _post;
        private string _title;
        private string _content;

        public EditPostVM()
        {
            
        }

        public EditPostVM(BlogPost post):this()
        {
            _post = post.Clone();//We don't want to trash main window

            Title = _post.Title;
            Content = _post.Content;
        }

        public string Content
        {
            get { return _content; }
            set
            {
                if (value == _content) return;
                _content = value;
                OnPropertyChanged();
            }
        }

        public String Title
        {
            get { return _title; }
            set
            {
                if (value == _title) return;
                _title = value;
                OnPropertyChanged();
            }
        }

        public ICommand SaveCommand
        {
            get
            {
                return RelayCommand.Create<Window>(
                    w =>
                    {
                        using (var unitOfWork = UnitOfWorkFactory.CreateInstance())
                        using (unitOfWork.TransactionManager.Begin()) // will auto rollback if not commited
                        {
                            var post = _post ?? new BlogPost(){UserId = StateHolder.CurrentUser.Id, Created = DateTimeOffset.Now};
                            
                            post.Content = Content;
                            post.Title = Title;

                            unitOfWork.BlogPostRepository.Upsert(post);

                            unitOfWork.TransactionManager.Commit();
                            //unitOfWork.TransactionManager.RollBack();
                                   
                            w.DialogResult = true;
                            w.Close();
                        }
                    },
                    _ => IsValid()
                );
            }
        }
        #region INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            var handler = PropertyChanged;
            if (handler != null)
            {
                if (Application.Current.Dispatcher.Thread != Thread.CurrentThread)
                    Application.Current.Dispatcher.BeginInvoke(handler, this, new PropertyChangedEventArgs(propertyName));
                else
                    handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        #endregion

        #region IDataErrorInfo

        public string this[string columnName]
        {
            get
            {
                switch (columnName)
                {
                    case "Content":
                        return String.IsNullOrWhiteSpace(Content) ? columnName + " can't be empty" : null;
                    case "Title":
                        return String.IsNullOrWhiteSpace(Title) ? columnName + " can't be empty" : null;
                }

                return null;
            }
        }

        public string Error { get; private set; }

        public bool IsValid()
        {
            return !String.IsNullOrWhiteSpace(Content) && !String.IsNullOrWhiteSpace(Title);
        }

        #endregion

    }
}