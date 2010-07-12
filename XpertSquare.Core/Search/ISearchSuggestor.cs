using System;

namespace XpertSquare.Core.Search
{
    public interface ISearchSuggestor
    {
        String GetSuggestion(String textForSuggestion);
    }
}
