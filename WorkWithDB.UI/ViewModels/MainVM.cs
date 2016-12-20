using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Windows;
using System.Windows.Input;
using JetBrains.Annotations;
using WorkWithDB.DAL.Abstract;
using WorkWithDB.Entity;
using WorkWithDB.Entity.Entities;
using WorkWithDB.Entity.Views;
using WorkWithDB.UI.Debug;
using WorkWithDB.UI.MVVM;
using WorkWithDB.UI.Views;

namespace WorkWithDB.UI.ViewModels
{
    public class MainVM :INotifyPropertyChanged
    {
        private IList<BlogPostWithAuthor> _allPosts;
        private BlogPostWithAuthor _currentSelection;
        private BlogPost _currentPost;
        private Visibility _isEditEnabled;

        public MainVM()
        {
            if (!GuiDebug.InDesignerMode)
            {
                _isEditEnabled = Visibility.Collapsed;

                RefreshList();
            }
        }

        public IList<BlogPostWithAuthor> AllPosts
        {
            get { return _allPosts; }
            set
            {
                if (Equals(value, _allPosts)) return;
                _allPosts = value;
                OnPropertyChanged();
            }
        }

        public BlogPostWithAuthor CurrentSelection
        {
            get { return _currentSelection; }
            set
            {
                if (Equals(value, _currentSelection)) return;
                _currentSelection = value;
                OnPropertyChanged();

                //Logic
                if (value != null)
                {
                    using (var unitOfWork = UnitOfWorkFactory.CreateInstance())
                    {
                        CurrentPost = unitOfWork.BlogPostRepository.GetById(value.Id);
                    }
                }
                else
                {
                    CurrentPost = null;
                }
            }
        }

        public BlogPost CurrentPost
        {
            get { return _currentPost; }
            set
            {
                if (Equals(value, _currentPost)) return;
                _currentPost = value;
                OnPropertyChanged();

                //Logic

                IsEditEnabled = value!= null && value.UserId == StateHolder.CurrentUser.Id ? Visibility.Visible : Visibility.Collapsed;
            }
        }

        public Visibility IsEditEnabled
        {
            get { return _isEditEnabled; }
            private set
            {
                if (value == _isEditEnabled) return;
                _isEditEnabled = value;
                OnPropertyChanged();
            }
        }

        #region Commands

        public ICommand RefreshListCommand
        {
            get { return RelayCommand.CreateVoid(RefreshList); }
        }

        public ICommand EditPostCommand
        {
            get { return RelayCommand.CreateVoid(EditPost); }
        }

        public ICommand AddPostCommand
        {
            get { return RelayCommand.CreateVoid(AddPost); }
        }

        private void RefreshList()
        {
            new Thread(() =>
            {
                using (var unitOfWork = UnitOfWorkFactory.CreateInstance())
                {
                    var selection = CurrentSelection;
                    var posts = unitOfWork.BlogPostRepository.GetAllWithUserNick();

                    CurrentSelection = null;
                    AllPosts = posts;
                    if (selection != null)
                    {
                        CurrentSelection = posts.FirstOrDefault(p => selection.Id == p.Id);
                    }
                    
                }
            }).Start();
        }

        private void EditPost()
        {
            if (CurrentPost != null && CurrentPost.UserId == StateHolder.CurrentUser.Id)
            {
                var result = new EditPost() {DataContext = new EditPostVM(CurrentPost)}.ShowDialog();
                if(result == true)
                    RefreshList();
            }
        }
        
        private void AddPost()
        {
            var result = new CreatePost().ShowDialog();
            if (result == true)
                RefreshList();
        }


        #endregion


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

    }
}