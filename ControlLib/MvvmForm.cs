using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;

namespace ControlLib
{
    public class MvvmForm : Control
    {
        private ContentPresenter contentPresenter;
        private Button addButton;
        private Button editButton;
        private Button saveButton;
        private Button cancelButton;
        private StackPanel footer;
        private object source;

        public DataTemplate DetailsTemplate
        {
            get { return (DataTemplate)GetValue(DetailsTemplateProperty); }
            set { SetValue(DetailsTemplateProperty, value); }
        }

        public static readonly DependencyProperty DetailsTemplateProperty =
            DependencyProperty.Register("DetailsTemplate", typeof(DataTemplate), typeof(MvvmForm), new PropertyMetadata(null));

        public DataTemplate AddNewTemplate
        {
            get { return (DataTemplate)GetValue(AddNewTemplateProperty); }
            set { SetValue(AddNewTemplateProperty, value); }
        }

        public static readonly DependencyProperty AddNewTemplateProperty =
            DependencyProperty.Register("AddNewTemplate", typeof(DataTemplate), typeof(MvvmForm), new PropertyMetadata(null));

        public DataTemplate EditTemplate
        {
            get { return (DataTemplate)GetValue(EditTemplateProperty); }
            set { SetValue(EditTemplateProperty, value); }
        }

        public static readonly DependencyProperty EditTemplateProperty =
            DependencyProperty.Register("EditTemplate", typeof(DataTemplate), typeof(MvvmForm), new PropertyMetadata(null));


        public object AddButtonContent
        {
            get { return (object)GetValue(AddButtonContentProperty); }
            set { SetValue(AddButtonContentProperty, value); }
        }

        public static readonly DependencyProperty AddButtonContentProperty =
            DependencyProperty.Register("AddButtonContent", typeof(object), typeof(MvvmForm), new PropertyMetadata("ADD"));

        public object EditButtonContent
        {
            get { return (object)GetValue(EditButtonContentProperty); }
            set { SetValue(EditButtonContentProperty, value); }
        }

        public static readonly DependencyProperty EditButtonContentProperty =
            DependencyProperty.Register("EditButtonContent", typeof(object), typeof(MvvmForm), new PropertyMetadata("EDIT"));

        public object SaveButtonContent
        {
            get { return (object)GetValue(SaveButtonContentProperty); }
            set { SetValue(SaveButtonContentProperty, value); }
        }

        public static readonly DependencyProperty SaveButtonContentProperty =
            DependencyProperty.Register("SaveButtonContent", typeof(object), typeof(MvvmForm), new PropertyMetadata("SAVE"));

        public object CancelButtonContent
        {
            get { return (object)GetValue(CancelButtonContentProperty); }
            set { SetValue(CancelButtonContentProperty, value); }
        }

        public static readonly DependencyProperty CancelButtonContentProperty =
            DependencyProperty.Register("CancelButtonContent", typeof(object), typeof(MvvmForm), new PropertyMetadata("CANCEL"));

        public object Source
        {
            get { return (object)GetValue(SourceProperty); }
            set { SetValue(SourceProperty, value); }
        }

        public static readonly DependencyProperty SourceProperty =
            DependencyProperty.Register("Source", typeof(object), typeof(MvvmForm), new PropertyMetadata(null));

        public FormState StartState
        {
            get { return (FormState)GetValue(StartStateProperty); }
            set { SetValue(StartStateProperty, value); }
        }

        public static readonly DependencyProperty StartStateProperty =
            DependencyProperty.Register("StartState", typeof(FormState), typeof(MvvmForm), new PropertyMetadata(FormState.Details));

        public event EventHandler ShowingDetails;
        public event EventHandler BeginAdd;
        public event EventHandler BeginEdition;
        public event EventHandler Saved;
        public event EventHandler Cancelled;

        static MvvmForm()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(MvvmForm), new FrameworkPropertyMetadata(typeof(MvvmForm)));
        }

        public MvvmForm()
        {
            this.Loaded += OnLoaded;
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            // assumme that DataContext is used to bind informations in templates
            this.source = this.Source != null ? this.Source : this.DataContext;
            this.contentPresenter.Content = this.source;

            SwitchToStartState();

            this.Loaded -= OnLoaded;
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            this.contentPresenter = GetTemplateChild("PART_Content") as ContentPresenter;
            this.addButton = GetTemplateChild("PART_AddButton") as Button;
            this.editButton = GetTemplateChild("PART_EditButton") as Button;
            this.saveButton = GetTemplateChild("PART_SaveButton") as Button;
            this.cancelButton = GetTemplateChild("PART_CancelButton") as Button;
            this.footer = GetTemplateChild("PART_Footer") as StackPanel;

            // visibility
            if (AddNewTemplate == null)
                this.addButton.Visibility = Visibility.Collapsed;

            if (EditTemplate == null)
                this.editButton.Visibility = Visibility.Collapsed;

            this.addButton.Click += OnAddButtonClick;
            this.editButton.Click += OnEditButtonClick;
            this.saveButton.Click += OnSaveButtonClick;
            this.cancelButton.Click += OnCancelButtonClick;
        }

        private void SwitchToStartState()
        {
            switch (StartState)
            {
                case FormState.Add:
                    Add();
                    break;
                case FormState.Edit:
                    Edit();
                    break;
                case FormState.Details:
                    ShowDetails();
                    break;
            }
        }

        private void OnAddButtonClick(object sender, RoutedEventArgs e)
        {
            Add();
        }

        private void OnEditButtonClick(object sender, RoutedEventArgs e)
        {
            Edit();
        }

        private void OnSaveButtonClick(object sender, RoutedEventArgs e)
        {
            Save();
        }

        private void OnCancelButtonClick(object sender, RoutedEventArgs e)
        {
            Cancel();
        }

        private void OnBeginAdd()
        {
            BeginAdd?.Invoke(this, EventArgs.Empty);
        }

        private void OnBeginEdit()
        {
            BeginEdition?.Invoke(this, EventArgs.Empty);
        }

        private void OnSaved()
        {
            Saved?.Invoke(this, EventArgs.Empty);
        }

        private void OnCancelled()
        {
            Cancelled?.Invoke(this, EventArgs.Empty);
        }

        private void UpdateStates(bool enableAddButton, bool enableEditButton, bool showFooter, DataTemplate template)
        {
            this.addButton.IsEnabled = enableAddButton;
            this.editButton.IsEnabled = enableEditButton;

            this.footer.Visibility = showFooter ? Visibility.Visible : Visibility.Collapsed;
            this.contentPresenter.ContentTemplate = template;
        }

        public void ShowDetails()
        {
            UpdateStates(true, true, false, DetailsTemplate);
            OnShowingDetails();
        }

        private void OnShowingDetails()
        {
            ShowingDetails?.Invoke(this, EventArgs.Empty);
        }

        private void BeginEdit()
        {
            if (source is IEditableObject)
            {
                ((IEditableObject)source).BeginEdit();
            }
        }

        private void CancelEdit()
        {
            if (source is IEditableObject)
            {
                ((IEditableObject)source).CancelEdit();
            }
        }

        public void Add()
        {
            BeginEdit();
            UpdateStates(false, false, true, AddNewTemplate);
            OnBeginAdd();
        }

        public void Edit()
        {
            BeginEdit();
            UpdateStates(false, false, true, EditTemplate);
            OnBeginEdit();
        }

        public void Save()
        {
            OnSaved();
            SwitchToStartState();
        }

        public void Cancel()
        {
            CancelEdit();
            OnCancelled();
            SwitchToStartState();
        }
    }

    public enum FormState
    {
        Add,
        Edit,
        Details
    }
}
