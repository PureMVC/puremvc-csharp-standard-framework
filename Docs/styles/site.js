function htmlToElement(html) {
    var template = document.createElement('template');
    html = html.trim(); // Never return a text node of whitespace as the result
    template.innerHTML = html;
    return template.content.firstChild;
}

function toggleVisibility(forItem, event){
    var moreItems = forItem.querySelector(".more_items");
    var anchor = forItem.querySelector("a.toggle");
    
    if(moreItems.classList.contains("hidden"))
    {
        moreItems.classList.remove("hidden");
        anchor.innerHTML = "see less";
    }
    else
    {
        moreItems.classList.add("hidden");
        anchor.innerHTML = "see all";
    }
}

function writeSeeMoreText(container) {
    var count = container.querySelectorAll(".more_items span").length;

    var created = htmlToElement('<p>' + count + ' derived types. <a href="#" class="toggle">see all</a></p>.');
    var anchor = created.querySelector("a");

    anchor.onclick = function(e) {
        toggleVisibility(container, e);
    };

    container.prepend(created);
}

(function() {
    var derivedTypesContainer = document.querySelector(".inheritance .derived");
    writeSeeMoreText(derivedTypesContainer);
    toggleVisibility(derivedTypesContainer);
})();