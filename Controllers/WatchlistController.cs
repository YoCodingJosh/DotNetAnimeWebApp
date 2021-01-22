using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

using JikanDotNet;

using AnimeWebApp.Models;

namespace AnimeWebApp.Controllers
{
    public class WatchlistController : Controller
    {
        [HttpGet]
        [Route("Watchlist/{username}")]
        public async Task<IActionResult> GetUserWatchlist(string username)
        {
            Jikan jikan = new Jikan();

            var watchlistData = await jikan.GetUserAnimeList(username);

            return View();
        }

        [HttpGet]
        [Route("Watchlist/{username}/{id}")]
        public async Task<IActionResult> GetAnimeDetailsFromUserWatchlist(string username, long id)
        {
            Jikan jikan = new Jikan();

            var watchlistData = await jikan.GetUserAnimeList(username);

            Anime details = null;
            AnimeDetailsModel model = new AnimeDetailsModel();

            foreach (var anime in watchlistData.Anime)
            {
                var watchStatus = anime.WatchingStatus;

                if (anime.MalId == id)
                {
                    details = await jikan.GetAnime(id);

                    model.ID = details.MalId;
                    model.Title = details.Title;
                    model.EnglishTitle = details.TitleEnglish;
                    model.Studios = string.Join(", ", details.Studios);
                    model.Synopsis = details.Synopsis;
                    model.ImageURL = details.ImageURL;
                    model.Genres = string.Join(", ", details.Genres);
                    model.Airing = details.Airing;
                    model.Premiered = details.Premiered;

                    int episodes;
                    if (!int.TryParse(details.Episodes, out episodes))
                    {
                        episodes = -1;
                    }
                    model.Episodes = episodes;

                    if (anime.Score == 0)
                    {
                        model.Score = null;
                    }
                    else
                    {
                        model.Score = anime.Score;
                    }

                    switch (anime.WatchingStatus)
                    {
                        case UserAnimeListExtension.Completed:
                            model.ListStatus = "Completed";
                            break;
                        case UserAnimeListExtension.Dropped:
                            model.ListStatus = "Dropped";
                            break;
                        case UserAnimeListExtension.OnHold:
                            model.ListStatus = "On Hold";
                            break;
                        case UserAnimeListExtension.PlanToWatch:
                            model.ListStatus = "Plan To Watch";
                            break;
                        case UserAnimeListExtension.Watching:
                            model.ListStatus = "Watching";
                            break;
                        default:
                            model.ListStatus = null;
                            break;
                    }

                    break;
                }
            }

            if (details == null)
            {
                // lmao what do?
                return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
            }

            

            return View("Views/Anime/AnimeDetailsView.cshtml", model);
        }
    }
}
