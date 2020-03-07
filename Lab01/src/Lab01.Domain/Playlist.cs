using System;
using System.Collections.Generic;
using System.Linq;

namespace Lab01.Domain
{
    public class Playlist
    {
        public event EventHandler<SongAddedEventArgs> SongAdded;

        public class SongAddedEventArgs : EventArgs
        {
            public string Title { get; set; }
        }

        protected virtual void OnSongAdded(SongAddedEventArgs e)
        {
            EventHandler<SongAddedEventArgs> handler = SongAdded;
            handler?.Invoke(this, e);
        }

        public Playlist()
        {
            Songs = new List<Song>();
        }

        public bool IsActive { get; set; }

        public string Title { get;set; }
        public List<Song> Songs { get; set; }

        public void AddSong(Song song)
        {
            if (Songs.Any(s => s.Title == song.Title))
                return;
                
            if (song.Artist == "Abba") throw new InvalidOperationException("Abba songs are forbidden.");

            Songs.Add(song);
            OnSongAdded(new SongAddedEventArgs{ Title = song.Title });

            Songs.Sort(new ArtistTitleComparer());
        }

        public void RemoveDuplicates() {
            var dict = new Dictionary<string, Song>();
            var dups = new List<Song>();

            Songs.ForEach(s =>  {
                var key = s.Title + s.Artist;
                if (dict.ContainsKey(key)) {
                    dups.Add(s);
                } else {
                    dict.Add(key, s);
                }
            });
        }
    }

    public class ArtistTitleComparer : Comparer<Song>
    {
        public override int Compare(Song x, Song y)
        {
            if (x.ReleaseDate.CompareTo(y.ReleaseDate) != 0) return x.ReleaseDate.CompareTo(y.ReleaseDate);
            if (x.Artist.CompareTo(y.Artist) != 0) return x.Artist.CompareTo(y.Artist);
            if (x.Title.CompareTo(y.Title) != 0) return x.Title.CompareTo(y.Title);
            return 0;
        }
    }
    public class Song
    {
        public DateTime ReleaseDate {get;set;}
        public string Artist { get; set; }
        public string Title { get; set; }
    }
}
