using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using PlayBall.GroupManagement.Business.Services;
using PlayBall.GroupManagement.Web.Demo;
using PlayBall.GroupManagement.Web.Mappings;
using PlayBall.GroupManagement.Web.ViewModel;

namespace PlayBall.GroupManagement.Web.Controllers
{
    // localhost:4999/groups
    [Route("groups")]
    public class GroupsController : Controller
    {
        #region fields
        private readonly IGroupService _groupService;
        private readonly SomeRootConfiguration _configuration;
        #endregion

        #region ctor
        public GroupsController(IGroupService groupService ,
            SomeRootConfiguration config)
        {
            _groupService = groupService;
            _configuration = config;
        }
        #endregion

        #region methods

        #region get all
        [HttpGet]
        [Route("")] // not needed because Index  would be used as default anyway
        public IActionResult Index()
        {
            return View(_groupService.GetAll().ToViewModel());
        }
        #endregion

        #region create

        [HttpGet]
        [Route("create")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [Route("")]
        [ValidateAntiForgeryToken]
        public IActionResult CreateGroup(GroupViewModel model)
        {
            _groupService.Add(model.ToServiceModel());

            return RedirectToAction("Index");
        }
        #endregion

        #region details
        [HttpGet]
        [Route("{id}")]
        public IActionResult Details(long id)
        {
            var group = _groupService.GetById(id);
            if (group is null) return NotFound();
            return View(group.ToViewModel());
        }
        #endregion

        #region update

        [HttpPost]
        [Route("{id}")]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(long id, GroupViewModel model)
        {
            var group = _groupService.Update(model.ToServiceModel());
            if (group is null) return NotFound();
            group.Name = model.Name;
            return RedirectToAction("Index");
        }

        #endregion

        #endregion
    }
}