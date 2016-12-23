using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace Engine
{
    public class PlayerQuest : INotifyPropertyChanged
    {
        private Quest questDetails;

        private bool isQuestCompleted;
        public Quest questInfo
        {
            get { return questDetails; }
            set
            {
                questDetails = value;
                WhenPropertyChanged("Details");//Don't mess with this
            }
        }
        public bool questCompletedOrFailed
        {
            get { return isQuestCompleted; }
            set
            {
                isQuestCompleted = value;
                WhenPropertyChanged("IsCompleted");//Don't mess with this
                WhenPropertyChanged("Name");//Don't mess with this
            }
        }

        public string nameOfQuest
        {
            get { return questInfo.questName; }
        }

        public PlayerQuest(Quest qDetails)
        {
            questInfo = qDetails;
            questCompletedOrFailed = false;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void WhenPropertyChanged(string name)
        {
            if(PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }
    }
}
