namespace lovejesuseveryday
{
    public class Helper
    {

        public static int GlobalChapterStartNum(int dayOfYear)
        // Gets the first chapter that should be read from a day of the year
        // May need adjustments
        /*
        Formula:
        3 chapters for 3 days
        4 chapters for 1 day

        Call this and use FindChapter for the value and FindChapter +1 and +2,
        If day of year % 4 == 0, do another +1

        274 * 3 = 822
        91 * 4 = 364
        total: 1186
        */
        {
            int carry = dayOfYear / 4;
            int globalChapterIndex = (dayOfYear * 3);
            globalChapterIndex += carry;
            globalChapterIndex -= 2;
            return globalChapterIndex;
        }

        public static string FindChapter(int globalIndex)
        {
            // Find the book and chapter
            int[] ChaptersPerBook = {
            50, 40, 27, 36, 34, 24, 21, 4, 31, 24,
            22, 25, 29, 36, 10, 13, 10, 42, 150, 31, 12, 8, 66, 52, 5, 48, 12,
            14, 3, 9, 1, 4, 7, 3, 3, 3, 2, 14, 4, 28, 16, 24, 21, 28, 16, 16,
            13, 6, 6, 4, 4, 5, 3, 6, 4, 3, 1, 13, 5, 5, 3, 3, 1, 1, 1, 22
            };
            string[] BookNames = {
            "GEN", "EXO", "LEV", "NUM", "DEU", "JOS", "JDG", "RUT", "1SA", "2SA", "1KI",
            "2KI", "1CH", "2CH", "EZR", "NEH", "EST", "JOB", "PSA", "PRO", "ECC", "SNG",
            "ISA", "JER", "LAM", "EZK", "DAN", "HOS", "JOL", "AMO", "OBA", "JON", "MIC",
            "NAM", "HAB", "ZEP", "HAG", "ZEC", "MAL", "MAT", "MRK", "LUK", "JHN", "ACT",
            "ROM", "1CO", "2CO", "GAL", "EPH", "PHP", "COL", "1TH", "2TH", "1TI", "2TI",
            "TIT", "PHM", "HEB", "JAS", "1PE", "2PE", "1JN", "2JN", "3JN", "JUD", "REV"
            };

            int bookChapter = 0;
            int x = 0;

            for (int i = 0; i < BookNames.Length; i++)
            {
                for (int j = 0; j < ChaptersPerBook[i]; j++)
                {
                    x++;
                    if (globalIndex == x)
                    {
                        for (int k = 0; k < i; k++)
                        {
                            bookChapter -= ChaptersPerBook[k];
                        }
                        bookChapter += x;
                        return (BookNames[i] + " " + bookChapter.ToString());
                    }
                }
            }
            return "Not found";
        }

        public static string FullBookName(string AAA)
        {
            string[] BookNames = {
            "GEN", "EXO", "LEV", "NUM", "DEU", "JOS", "JDG", "RUT", "1SA", "2SA", "1KI",
            "2KI", "1CH", "2CH", "EZR", "NEH", "EST", "JOB", "PSA", "PRO", "ECC", "SNG",
            "ISA", "JER", "LAM", "EZK", "DAN", "HOS", "JOL", "AMO", "OBA", "JON", "MIC",
            "NAM", "HAB", "ZEP", "HAG", "ZEC", "MAL", "MAT", "MRK", "LUK", "JHN", "ACT",
            "ROM", "1CO", "2CO", "GAL", "EPH", "PHP", "COL", "1TH", "2TH", "1TI", "2TI",
            "TIT", "PHM", "HEB", "JAS", "1PE", "2PE", "1JN", "2JN", "3JN", "JUD", "REV"
            };
            string[] FullBookNames = {
            "Genesis", "Exodus", "Leviticus", "Numbers", "Deuteronomy", "Joshua", "Judges", "Ruth", "1 Samuel", "2 Samuel", "1 Kings",
            "2 Kings", "1 Chronicles", "2 Chronicles", "Ezra", "Nehemiah", "Esther", "Job", "Psalms", "Proverbs", "Ecclesiastes", "Song of Songs",
            "Isaiah", "Jeremiah", "Lamentations", "Ezekiel", "Daniel", "Hosea", "Joel", "Amos", "Obadiah", "Jonah", "Micah",
            "Nahum", "Habakkuk", "Zephaniah", "Haggai", "Zechariah", "Malachi", "Matthew", "Mark", "Luke", "John", "Acts",
            "Romans", "1 Corinthians", "2 Corinthians", "Galatians", "Ephesians", "Philippians", "Colossians", "1 Thessalonians", "2 Thessalonians", "1 Timothy", "2 Timothy",
            "Titus", "Philemon", "Hebrews", "James", "1 Peter", "2 Peter", "1 John", "2 John", "3 John", "Jude", "Revelation"
            };
            for (int i = 0; i < FullBookNames.Length; i++) {
                if (AAA == BookNames[i]) return FullBookNames[i];
            }
            return "Not found";
        }
    }
}
