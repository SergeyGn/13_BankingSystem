using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace _13_BankingSystem
{
   public class Messages:INotifyPropertyChanged
    {
    
        public event PropertyChangedEventHandler PropertyChanged;

        private List<string> messageText;
        

        public List<string> MessageText 
        {
            get { return this.messageText; }
            set
            {
                this.messageText = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(this.MessageText.Count.ToString()));
            }
        }

        public void Added(string text)
        {
            MessageText.Add(text);
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(this.MessageText.Count.ToString()));
        }
    }
}
