using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using JikanDotNet;
using AnimeWebApp.Models;

namespace AnimeWebApp.Controllers
{
    public class AnimeController : Controller
    {
        [HttpGet]
        [Route("Anime/{id}")]
        public async Task<IActionResult> Index(long id)
        {
            AnimeDetailsModel model = new AnimeDetailsModel();

            Jikan jikan = new Jikan();

            var anime = await jikan.GetAnime(id);

            model.ID = anime.MalId;
            model.Title = anime.Title;
            model.EnglishTitle = anime.TitleEnglish;
            model.Studios = string.Join(", ", anime.Studios);
            model.Synopsis = anime.Synopsis;
            model.ImageURL = anime.ImageURL;
            model.Genres = string.Join(", ", anime.Genres);
            model.Airing = anime.Airing;
            model.Premiered = anime.Premiered;

            int episodes;
            if (!int.TryParse(anime.Episodes, out episodes))
            {
                episodes = -1;
            }
            model.Episodes = episodes;

            return View("AnimeDetailsView", model);
        }
    }
}
