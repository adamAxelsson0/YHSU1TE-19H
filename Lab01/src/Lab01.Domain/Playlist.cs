using System;
using System.Collections.Generic;

namespace Lab01.Domain
{
    public class Playlist
    {
        public List<Song> Songs {get;set;}
        
        public Playlist(string title) {
            IsActive = true;
            this.Title = "abc" + title;
            Songs = new List<Song>();
            //
        }

        public string Title {get;set;}

        public bool IsActive {get;set;}

        public class Song {

        }
    }
}
