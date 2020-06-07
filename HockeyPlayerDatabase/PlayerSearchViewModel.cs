using System.Collections.Generic;
using HockeyPlayerDatabase.Model;

namespace HockeyPlayerDatabase
{
    public class PlayerSearchViewModel
    {
        public PlayerSearchModel SearchModel { get; set; }
        

        public HockeyContext DbContext;

        public PlayerSearchViewModel()
        {
            SearchModel = new PlayerSearchModel();
            DbContext = new HockeyContext();
            DbContext.InitializeFilteredPlayers(); 
        }

        public void FilterPlayers(PlayerSearchModel searchModel)
        {
            DbContext.FilterPlayers(searchModel);
        }
    }
}
