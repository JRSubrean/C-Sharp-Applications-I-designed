using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace Engine
{
    public class LivingCreature : INotifyPropertyChanged
    /*Base class for the Player class and Monster class. Both of those classes inherit
     from this class.
     
     The : NotifyPropertyChanges means that the LivingCreature class needs to
     implement the NotifyPropertyChanges interface. The NotifyPropertyChanges
     interface is what the .NET framework uses for databinding notifications, when a
     property value changes.*/
    {
        private int theCurrentHitPoints;

        public int currentHitPoints
        {
            get { return theCurrentHitPoints; }
            set
            {
                theCurrentHitPoints = value;
                WhenPropertyChanged("CurrentHitPoints");
            }
        }
        /*currentHitPoints is now a property with a backing variable.

        The currentHitPoints property is now also a wrapper for the private variable
        theCurrentHitPoints. When the property is set to a value, the
        WhenPropertyChanged() function will be run, the PropertyHasChanged event will
        be raised, and the UI will update the label.*/

        public int maximumHitPoints { get; set; }

        public LivingCreature(int currentHP, int maxHP)
        {
            currentHitPoints = currentHP;
            maximumHitPoints = maxHP;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        /*The PropertyHasChanged event is what the UI will "subscribe" to.*/

        protected void WhenPropertyChanged(string name)
        /*The WhenPropertyChanged function checks if anything's subscribed to the
         event. If nothing's subscribed, then PropertyHasChanged will be null.
         Otherwise, another class wants to be notified of changes, so the line within
         the if statement below will run, and the PropertyHasChanged event will
         "be raised", meaning the notification will be sent out.*/
        {
            if(PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }
    }
}
