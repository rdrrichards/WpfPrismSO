using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Interactivity;

namespace WpfPrismSO
{
    public class DragDropBehavior : Behavior<TextBox>
    {
        private bool _isDragging;
        private IDataObject _dataObject;
        private Type _dataType;

        public ICommand DragOverCommand { get { return (ICommand)GetValue(DragOverCommandProperty); } set { SetValue(DragOverCommandProperty, value); } }
        public static readonly DependencyProperty DragOverCommandProperty = DependencyProperty.Register("DragOverCommand", typeof(ICommand), typeof(DragDropBehavior), new PropertyMetadata(null));

        public ICommand DropCommand { get { return (ICommand)GetValue(DropCommandProperty); } set { SetValue(DropCommandProperty, value); } }
        public static readonly DependencyProperty DropCommandProperty = DependencyProperty.Register("DropCommand", typeof(ICommand), typeof(DragDropBehavior), new PropertyMetadata(null));


        protected override void OnAttached()
        {
            this.AssociatedObject.AllowDrop = true;
            this.AssociatedObject.PreviewMouseLeftButtonDown += AssociatedObject_PreviewMouseLeftButtonDown;
            this.AssociatedObject.PreviewMouseMove += AssociatedObject_PreviewMouseMove;
            this.AssociatedObject.PreviewMouseLeftButtonUp += AssociatedObject_PreviewMouseLeftButtonUp;
            //this.AssociatedObject.Drop += AssociatedObject_Drop;
            base.OnAttached();
        }

        private void AssociatedObject_Drop(object sender, DragEventArgs e)
        {
            throw new NotImplementedException();
        }

        void AssociatedObject_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            // get data from mouse position
            var data = sender as TextBox;
            
            if (data != null)
            {
                _dataType = data.GetType();
                _dataObject = new DataObject(_dataType, data);
                _isDragging = true; // valid data found, set dragging to true
            }
        }

        void AssociatedObject_PreviewMouseMove(object sender, MouseEventArgs e)
        {
            if (_isDragging)
            {
                // let viewmodel know of the drag
                this.DragOverCommand.Execute(_dataObject.GetData(_dataType));

                // execute drag
                //var eff = DragDrop.DoDragDrop(this.AssociatedObject, _dataObject, DragDropEffects.Copy | DragDropEffects.Move);
                // thread waits for DragDrop to finish...

                //Drop();
            }
        }

        void AssociatedObject_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            _isDragging = false;
            Drop();
        }

        private void Drop()
        {
            // let viewmodel know of the drop
            this.DropCommand.Execute(_dataObject.GetData(_dataType));

            _isDragging = false;
        }
    }
}