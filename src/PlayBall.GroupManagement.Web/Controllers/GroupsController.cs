﻿using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using PlayBall.GroupManagement.Web.Demo;
using PlayBall.GroupManagement.Web.ViewModel;

namespace PlayBall.GroupManagement.Web.Controllers
{
    // localhost:4999/groups
    [Route("groups")]
    public class GroupsController : Controller
    {
        #region fields
        private readonly IGroupGenerator _groupGenerator;
        private static readonly List<GroupViewModel> Groups =
        new List<GroupViewModel> { new GroupViewModel { Id = 1, Name = "Sample Group" } };
        #endregion
        #region ctor

        public GroupsController(IGroupGenerator groupGenerator)
        {
            _groupGenerator = groupGenerator;
        }
        #endregion

        [HttpGet]
        [Route("")] // not needed because Index  would be used as default anyway
        public IActionResult Index()
        {
            return View(Groups);
        }

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
            model.Id = _groupGenerator.Next();
            Groups.Add(model);
            return RedirectToAction("Index");
        }
        [HttpGet]
        [Route("{id}")]
        public IActionResult Details(long id)
        {
            var group = Groups.SingleOrDefault(x => x.Id == id);
            if (group is null) return NotFound();
            return View(group);
        }

        [HttpPost]
        [Route("{id}")]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(long id, GroupViewModel model)
        {
            var group = Groups
                .SingleOrDefault(x => x.Id == id);
            if (group is null) return NotFound();
            group.Name = model.Name;
            return RedirectToAction("Index");
        }

    }
}