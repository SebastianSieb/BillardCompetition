
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace BillardCompetition.Model
{
    /// <summary>
    /// This class implements the INotifyPropertyChanged interface to manage the data bindings.
    /// </summary>
    public abstract class BaseModel : INotifyPropertyChanged
    {
        /// <summary>
        /// The event of a changed property.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// This method notifies the View that a property has Changed.
        /// </summary>
        /// <param name="propertyName">The name of the changed property.</param>
        public void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            if (PropertyChanged == null) return;

            PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

