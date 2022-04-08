
var substringMatcher = function (strs) {
    return function findMatches(q, cb) {
        var matches, substringRegex;

        // an array that will be populated with substring matches
        matches = [];

        // regex used to determine if a string contains the substring `q`
        substrRegex = new RegExp(q, 'i');

        // iterate through the pool of strings and for any string that
        // contains the substring `q`, add it to the `matches` array
        $.each(strs, function (i, str) {
            if (substrRegex.test(str)) {
                matches.push(str);
            }
        });

        cb(matches);
    };
};
var dataaa = "";
function ASPSnippetsPager(a, b) { var c = '<a style = "cursor:pointer" class="page" page = "{1}">{0}</a>'; var d = "<span>{0}</span>"; var e, f, g; var g = 5; var h = Math.ceil(b.RecordCount / b.PageSize); if (b.PageIndex > h) { b.PageIndex = h } var i = ""; if (h > 1) { f = h > g ? g : h; e = b.PageIndex > 1 && b.PageIndex + g - 1 < g ? b.PageIndex : 1; if (b.PageIndex > g % 2) { if (b.PageIndex == 2) f = 5; else f = b.PageIndex + 2 } else { f = g - b.PageIndex + 1 } if (f - (g - 1) > e) { e = f - (g - 1) } if (f > h) { f = h; e = f - g + 1 > 0 ? f - g + 1 : 1 } var j = (b.PageIndex - 1) * b.PageSize + 1; var k = j + b.PageSize - 1; if (k > b.RecordCount) { k = b.RecordCount } i = "<b>Records " + (j == 0 ? 1 : j) + " - " + k + " of " + b.RecordCount + "</b> "; if (b.PageIndex > 1) { i += c.replace("{0}", "<<").replace("{1}", "1"); i += c.replace("{0}", "<").replace("{1}", b.PageIndex - 1) } for (var l = e; l <= f; l++) { if (l == b.PageIndex) { i += d.replace("{0}", l) } else { i += c.replace("{0}", l).replace("{1}", l) } } if (b.PageIndex < h) { i += c.replace("{0}", ">").replace("{1}", b.PageIndex + 1); i += c.replace("{0}", ">>").replace("{1}", h) } } a.html(i); try { a[0].disabled = false } catch (m) { } } (function (a) { a.fn.ASPSnippets_Pager = function (b) { var c = {}; var b = a.extend(c, b); return this.each(function () { ASPSnippetsPager(a(this), b) }) } })(jQuery);





// -------- Prefatch --------

var countries = new Bloodhound({
    datumTokenizer: Bloodhound.tokenizers.whitespace,
    queryTokenizer: Bloodhound.tokenizers.whitespace,
    // url points to a json file that contains an array of country names, see
    // https://github.com/twitter/typeahead.js/blob/gh-pages/data/countries.json
    prefetch: '../plugins/bower_components/typeahead.js-master/countries.json'
});

// passing in `null` for the `options` arguments will result in the default
// options being used
$('#prefetch .typeahead').typeahead(null, {
    name: 'countries',
    source: countries
});

// -------- Custom --------

var nflTeams = new Bloodhound({
    datumTokenizer: Bloodhound.tokenizers.obj.whitespace('team'),
    queryTokenizer: Bloodhound.tokenizers.whitespace,
    identify: function (obj) { return obj.team; },
    prefetch: '../plugins/bower_components/typeahead.js-master/nfl.json'
});

function nflTeamsWithDefaults(q, sync) {
    if (q === '') {
        sync(nflTeams.get('Detroit Lions', 'Green Bay Packers', 'Chicago Bears'));
    }

    else {
        nflTeams.search(q, sync);
    }
}

$('#default-suggestions .typeahead').typeahead({
    minLength: 0,
    highlight: true
},
{
    name: 'nfl-teams',
    display: 'team',
    source: nflTeamsWithDefaults
});

// -------- Multiple --------

var nbaTeams = new Bloodhound({
    datumTokenizer: Bloodhound.tokenizers.obj.whitespace('team'),
    queryTokenizer: Bloodhound.tokenizers.whitespace,
    prefetch: '../plugins/bower_components/typeahead.js-master/nba.json'
});

var nhlTeams = new Bloodhound({
    datumTokenizer: Bloodhound.tokenizers.obj.whitespace('team'),
    queryTokenizer: Bloodhound.tokenizers.whitespace,
    prefetch: '../plugins/bower_components/typeahead.js-master/nhl.json'
});

$('#multiple-datasets .typeahead').typeahead({
    highlight: true
},
{
    name: 'nba-teams',
    display: 'team',
    source: nbaTeams,
    templates: {
        header: '<h3 class="league-name">NBA Teams</h3>'
    }
},
{
    name: 'nhl-teams',
    display: 'team',
    source: nhlTeams,
    templates: {
        header: '<h3 class="league-name">NHL Teams</h3>'
    }
});

// -------- Scrollable --------



$('#scrollable-dropdown-menu .typeahead').typeahead(null, {
    name: 'states',
    limit: 10,
    source: states
});
