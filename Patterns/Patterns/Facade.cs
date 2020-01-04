using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Patterns
{
    class ProfessionalEducation
    {
        Reading reading; 
        Listening listening;
        Watching watching;
        public ProfessionalEducation(Reading reading, Listening listening, Watching watching)
        {
            this.reading = reading;
            this.listening = listening;
            this.watching = watching;
        }

        public void Commmmon()
        {
            reading.ReadBooks();
            listening.ListenAudio();
            watching.WatchVideos();            
        }
    }

    class Reading
    {
        public void ReadBooks()
        {
            Console.WriteLine("Read prof books");
        }
    }

    class Listening
    {
        public void ListenAudio()
        {
            Console.WriteLine("Listen some audios");
        }
    }

    class Watching
    {
        public void WatchVideos()
        {
            Console.WriteLine("Watch some videos");
        }
    }

}
