using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TitleHunt.Models;

namespace TitleHunt.Controllers
{ 
    public class TitleController : Controller
    {
        private TitlesEntities db = new TitlesEntities();

        //
        // GET: /Title/

        public ViewResult Index()
        {
            var title = from t in db.Titles
                        join tg in db.TitleGenres on t.TitleId equals tg.TitleId
                        join g in db.Genres on tg.GenreId equals g.Id
                        join s in db.StoryLines on t.TitleId equals s.TitleId
                        join tp in db.TitleParticipants on t.TitleId equals tp.TitleId
                        join p in db.Participants       on tp.ParticipantId equals p.Id
                        where (tp.RoleType == "Producer") || (tp.RoleType == "Director")
                        select new TitleView
                        {
                            TitleId = t.TitleId,
                            TitleName = t.TitleName,
                            ReleaseYear = t.ReleaseYear,
                            GenreName = g.Name,
                            Description = s.Description.Substring(0, 100),
                            Participant = p.Name,
                            Role = tp.RoleType
                        };

            return View(title.ToList().OrderBy(a => a.TitleName).ThenBy(b => b.TitleId).ThenBy(c => c.Role));
        }

        //
        // GET: /Title/Details/5

        public ViewResult Details(int id)
        {
            
            var title = from t in db.Titles
                        join tg in db.TitleGenres on t.TitleId equals tg.TitleId
                        join g in db.Genres on tg.GenreId equals g.Id
                        join s in db.StoryLines on t.TitleId equals s.TitleId
                        join tp in db.TitleParticipants on t.TitleId equals tp.TitleId
                        join p in db.Participants on tp.ParticipantId equals p.Id
                        join aw in db.Awards on t.TitleId equals aw.TitleId into awt
                        from aw in awt.DefaultIfEmpty()

                        where t.TitleId == id
                        
                        select new TitleView
                        {
                            TitleId = t.TitleId,
                            TitleName = t.TitleName,
                            ReleaseYear = t.ReleaseYear,
                            GenreName = g.Name,
                            Description = s.Description,
                            Participant = p.Name,
                            Role = tp.RoleType,
                            ParticipantType = p.ParticipantType,
                            IsKey = tp.IsKey,
                            IsOnScreen = tp.IsOnScreen,
                            ParticipantId = tp.ParticipantId,
                            Award = aw.Award1,
                            AwardCompany = aw.AwardCompany,
                            AwardWon = aw.AwardWon,
                            AwardYear = aw.AwardYear
                        };

            return View(title.ToList().OrderBy(a => a.TitleName).ThenBy(b => b.TitleId).ThenBy(c => c.ParticipantId).ThenBy(d => d.AwardWon).ThenBy(e => e.Award).ThenBy(f => f.AwardCompany));

        }

        //public ViewResult Details(int id)
        //{
        //    var title = from t in db.Titles
        //                join tg in db.TitleGenres on t.TitleId equals tg.TitleId
        //                join g in db.Genres on tg.GenreId equals g.Id
        //                join s in db.StoryLines on t.TitleId equals s.TitleId
        //                join tp in db.TitleParticipants on t.TitleId equals tp.TitleId
        //                join p in db.Participants on tp.ParticipantId equals p.Id
        //                join aw in db.Awards on t.TitleId equals aw.TitleId into awt
        //                from aw in awt.DefaultIfEmpty()

        //                where t.TitleId == id

        //                select new TitleView
        //                {
        //                    TitleId = t.TitleId,
        //                    TitleName = t.TitleName,
        //                    ReleaseYear = t.ReleaseYear,
        //                    GenreName = g.Name,
        //                    Description = s.Description,
        //                    Participant = p.Name,
        //                    Role = tp.RoleType,
        //                    ParticipantType = p.ParticipantType,
        //                    IsKey = tp.IsKey,
        //                    IsOnScreen = tp.IsOnScreen,
        //                    ParticipantId = tp.ParticipantId,
        //                    Award = aw.Award1,
        //                    AwardCompany = aw.AwardCompany,
        //                    AwardWon = aw.AwardWon,
        //                    AwardYear = aw.AwardYear
        //                };

        //    var awards = from t in db.Titles
        //                 join aw in db.Awards on t.TitleId equals aw.TitleId into awt
        //                 from aw in awt.DefaultIfEmpty()

        //                 where t.TitleId == id

        //                 select new AwardsModel
        //                 {
        //                     TitleId = t.TitleId,
        //                     TitleName = t.TitleName,
        //                     ReleaseYear = t.ReleaseYear,

        //                     Award = aw.Award1,
        //                     AwardCompany = aw.AwardCompany,
        //                     AwardWon = aw.AwardWon,
        //                     AwardYear = aw.AwardYear
        //                 };
        //    var mainModel = new MainModel() { TitleList = title.ToList(), AwardList= awards.ToList() };

        //    return View(mainModel);

        //}

        //public void CreateAwardsView(int id) 
        //{
        //    var awards = from t in db.Titles
        //                join aw in db.Awards on t.TitleId equals aw.TitleId into awt
        //                from aw in awt.DefaultIfEmpty()

        //                where t.TitleId == id

        //                select new AwardsModel
        //                {
        //                    TitleId = t.TitleId,
        //                    TitleName = t.TitleName,
        //                    ReleaseYear = t.ReleaseYear,
                            
        //                    Award = aw.Award1,
        //                    AwardCompany = aw.AwardCompany,
        //                    AwardWon = aw.AwardWon,
        //                    AwardYear = aw.AwardYear
        //                };        
        //}

        //
        // GET: /Title/Create

        public ActionResult Create()
        {
            return View();
        } 

        //
        // POST: /Title/Create

        [HttpPost]
        public ActionResult Create(Title title)
        {
            if (ModelState.IsValid)
            {
                db.Titles.AddObject(title);
                db.SaveChanges();
                return RedirectToAction("Index");  
            }

            return View(title);
        }
        
        //
        // GET: /Title/Edit/5
 
        public ActionResult Edit(int id)
        {
            Title title = db.Titles.Single(t => t.TitleId == id);
            return View(title);
        }

        //
        // POST: /Title/Edit/5

        [HttpPost]
        public ActionResult Edit(Title title)
        {
            if (ModelState.IsValid)
            {
                db.Titles.Attach(title);
                db.ObjectStateManager.ChangeObjectState(title, EntityState.Modified);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(title);
        }

        //
        // GET: /Title/Delete/5
 
        public ActionResult Delete(int id)
        {
            Title title = db.Titles.Single(t => t.TitleId == id);
            return View(title);
        }

        //
        // POST: /Title/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {            
            Title title = db.Titles.Single(t => t.TitleId == id);
            db.Titles.DeleteObject(title);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }

        public ActionResult Browse(string genre) 
        {

            var title = from t in db.Titles
                        join tg in db.TitleGenres on t.TitleId equals tg.TitleId
                        join g in db.Genres on tg.GenreId equals g.Id
                        join s in db.StoryLines on t.TitleId equals s.TitleId
                        join tp in db.TitleParticipants on t.TitleId equals tp.TitleId
                        join p in db.Participants on tp.ParticipantId equals p.Id
                        where ((tp.RoleType == "Producer") || (tp.RoleType == "Director"))
                        && (g.Name == genre)
                        select new TitleView
                        {
                            TitleId = t.TitleId,
                            TitleName = t.TitleName,
                            ReleaseYear = t.ReleaseYear,
                            GenreName = g.Name,
                            Description = s.Description.Substring(0, 100),
                            Participant = p.Name,
                            Role = tp.RoleType
                        };

            return View(title.ToList().OrderBy(a => a.TitleName).ThenBy(b => b.TitleId).ThenBy(c => c.Role));

        }

        public ActionResult AlphaBrowse(string alpha)
        {
        var title = from t in db.Titles
                    join tg in db.TitleGenres on t.TitleId equals tg.TitleId
                    join g in db.Genres on tg.GenreId equals g.Id
                    join s in db.StoryLines on t.TitleId equals s.TitleId
                    join tp in db.TitleParticipants on t.TitleId equals tp.TitleId
                    join p in db.Participants on tp.ParticipantId equals p.Id
                    where ((tp.RoleType == "Producer") || (tp.RoleType == "Director"))
                    && (t.TitleName.StartsWith(alpha))
                    select new TitleView
                    {
                        TitleId = t.TitleId,
                        TitleName = t.TitleName,
                        ReleaseYear = t.ReleaseYear,
                        GenreName = g.Name,
                        Description = s.Description.Substring(0, 100),
                        Participant = p.Name,
                        Role = tp.RoleType
                    };
            return View(title.ToList().OrderBy(a => a.TitleName).ThenBy(b => b.TitleId).ThenBy(c => c.Role));
        }

        public ActionResult ComingSoon() 
        {
            return View();
        }



        public ActionResult SearchDB(FormCollection frmCol)
        {
            string searc= frmCol["Search"];
            var title = from t in db.Titles
                        join tg in db.TitleGenres on t.TitleId equals tg.TitleId
                        join g in db.Genres on tg.GenreId equals g.Id
                        join s in db.StoryLines on t.TitleId equals s.TitleId
                        join tp in db.TitleParticipants on t.TitleId equals tp.TitleId
                        join p in db.Participants on tp.ParticipantId equals p.Id
                        where ((tp.RoleType == "Producer") || (tp.RoleType == "Director"))
                        && (t.TitleName.Contains(searc))
                        select new TitleView
                        {
                            TitleId = t.TitleId,
                            TitleName = t.TitleName,
                            ReleaseYear = t.ReleaseYear,
                            GenreName = g.Name,
                            Description = s.Description.Substring(0, 100),
                            Participant = p.Name,
                            Role = tp.RoleType
                        };

            return View(title.ToList().OrderBy(a => a.TitleName).ThenBy(b => b.TitleId).ThenBy(c => c.Role));
        }
    }
}