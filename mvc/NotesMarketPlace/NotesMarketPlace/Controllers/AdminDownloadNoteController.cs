using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NotesMarketPlace.Controllers
{
    [RoutePrefix("Admin")]
    public class AdminDownloadNoteController : Controller
    {
        readonly NotesMarketPlaceEntities db;
        public AdminDownloadNoteController()
        {
            db = new NotesMarketPlaceEntities();
        }

        [Authorize(Roles = "Admin, SuperAdmin, Member")]
        [HttpGet]
        // GET: AdminDownloadNote
        public ActionResult AdminDownload(int noteId, int userId)
        {
            if (Session["ID"] == null)
            {
                return RedirectToAction("Login", "Account");
            }

            var user = db.Users.FirstOrDefault(x => x.Email == User.Identity.Name);
                       
            //count notes for zip or simple note download
            var note = db.SellerNotes.Find(noteId);
            var count = db.SellerNotesAttachements.Where(x => x.NoteID == noteId).Count();
            string notesattachementpath = "~/Members/" + userId + "/" + noteId + "/Attachements/";
            if (count > 1)
            {
                var noteattachement = db.SellerNotesAttachements.Where(x => x.NoteID == note.ID).ToList();
            }

            //full path for download file
            string filename = db.SellerNotesAttachements.FirstOrDefault(x => x.NoteID == noteId).FileName;
            string attachmentspath = notesattachementpath + filename;
            string fullpath = System.IO.Path.Combine(notesattachementpath, filename);


            //for multiple file
            if (count > 1)
            {
                string path = Server.MapPath(notesattachementpath);

                DirectoryInfo dir = new DirectoryInfo(path);

                using (var memoryStream = new MemoryStream())
                {
                    using (var ziparchive = new ZipArchive(memoryStream, System.IO.Compression.ZipArchiveMode.Create, true))
                    {
                        foreach (var item in dir.GetFiles())
                        {
                            string filepath = path + item.ToString();
                            ziparchive.CreateEntryFromFile(filepath, item.ToString());
                        }
                    }
                    return File(memoryStream.ToArray(), "application/zip", note.Title + ".zip");
                }
            }

            //for only one file
            return File(fullpath, "application/pdf", filename);
        }
    }
}