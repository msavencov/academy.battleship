﻿
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web.Http;
using System.Web.Mvc;
using Academy.BattleShip.Service;
using Academy.BattleShip.Service.Exceptions;
using Academy.BattleShip.Service.Models;
using Academy.BattleShip.Service.Services;
using Academy.BattleShip.Web.Models;

namespace Academy.BattleShip.Web.Controllers
{
    public partial class HomeController
    {
        [System.Web.Mvc.HttpGet]
        public ActionResult Index(string key)
        {
            key = string.IsNullOrEmpty(key) ? string.Empty : key;

            var model = new RegisterModel();
            Player player = null;

            try
            {
                player = _playerService.Find(key);
                model.Key = player.Key;
                model.Name = player.Name;
                model.Init(player.Cells.ToList());
            }
            catch (PlayerNotFoundException exception)
            {
                model.Init(new List<Point>());
            }
            
            if (player == null && Player.KeyRegex.Match(key).Success)
            {
                ModelState.AddModelError(string.Empty, "Player with key: " + key + " not found.");
            }

            return View(model);
        }
        
        [System.Web.Http.HttpPost]
        public ActionResult Index([FromBody] RegisterModel model)
        {
            Player player = null;
            
            try
            {
                player = _playerService.Find(model.Key);
            }
            catch (PlayerNotFoundException)
            {
                try
                {
                    player = _playerService.Register(model.Name);
                    model.Key = player.Key;
                }
                catch (EntityValidationException exception)
                {
                    _addValidationErrors(exception);
                }
            }

            if (player == null)
            {
                return View(model);
            }
            
            if (ModelState.IsValid)
            {
                var cells = new List<Point>();
                ShipMap.Iterate((x, y) =>
                {
                    if (model.ShipMap[x][y])
                    {
                        cells.Add(new Point {X = x, Y = y});
                    }
                });

                try
                {
                    _playerService.UpdateMap(player.Key, cells);
                }
                catch (EntityValidationException exception)
                {
                    _addValidationErrors(exception);
                }
                catch (MapValidationException exception)
                {
                    ModelState.AddModelError(string.Empty, exception.Message);
                }
                catch (ShipsCollisionException exception)
                {
                    ModelState.AddModelError(string.Empty, exception.Message);
                }
            }
            
            return View(model);
        }

        private void _addValidationErrors(EntityValidationException exception)
        {
            exception.Errors.ToList().ForEach(t => ModelState.AddModelError(string.Empty, t.ErrorMessage));
        }
    }

    public partial class HomeController : Controller
    {
        private readonly IPlayerService _playerService;
        
        public HomeController(IPlayerService playerService)
        {
            _playerService = playerService;
        }
    }
}