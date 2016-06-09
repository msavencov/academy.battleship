"use strict";

function Map(map, form) {
    this.form = form;
    this.map = map || [[]];
}

Map.prototype.init = function () {
    var form = $(this.form.selector);
    var panel = form.find(".panel-body");
    var topOffset = panel.position().top;

    var height = $(window).height() - topOffset;
    var width = form.width();

    var mapSize = (height > width ? width : height) * 0.90;
    var boxSize = (mapSize / 10) - 4;

    panel.css("width", mapSize);
    panel.css("height", mapSize);
    form.width(mapSize + 20);
    form.css("margin", "auto auto");

    for (var i = 0; i < 10; i++) {
        for (var j = 0; j < 10; j++) {
            var item = this.map[j][i] || false;
            var data = { x: j, y: i, value: item };
            var square = $("<div/>").addClass("square");
            square.height(boxSize).width(boxSize);
            square.data("position", data);

            if (item) {
                square.addClass("selected");
            }

            panel.append(square);

            $("input[name='ShipMap[" + data.x + "][" + data.y + "]']").val(data.value ? "True" : "False");
        }
        panel.append($("<div/>").css("clear", "both"));
    }

    $(".square").on("click", function () {
        var data = $(this).data("position") || {};
        data.value = !data.value;
        if (data.value) {
            $(this).addClass("selected");
        } else {
            $(this).removeClass("selected");
        }
        $(this).data("position", data);
        
        $("input[name='ShipMap[" + data.x + "][" + data.y + "]']").val(data.value ? "True" : "False");
    });
    //debugger;
};

Map.prototype.change = function(x, y) {
    debugger;
};

Map.prototype.reset = function() {
    this.map = [[]];
    var items = $(".square");
};

