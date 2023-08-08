$('.generator-configuration-js').change(updateTableData)

$('#random-seed-js').click(e => {
    e.preventDefault()
    $('#seed-js').val(getRandomInt(2_147_483_648))
    updateTableData()
})

var tableUpdateExecuted = true
var nextPage = 2

$(window).scroll(async () => {
    const userScroll = $(window).scrollTop()
    switchToTopButton(userScroll)
    if (userScroll + $(window).height() >= $(document).height() - 1 && tableUpdateExecuted) {
        tableUpdateExecuted = false
        await appendTableData()
    }
})

async function appendTableData() {
    $.post('/getUsersData', getGeneratorConfiguration(page = nextPage++, pageSize = 10), function (data) { 
        $('#users-table-body-js').append(data)
        tableUpdateExecuted = true
    })
}

async function updateTableData() {
    nextPage = 2
    $.post('/getUsersData', getGeneratorConfiguration(), function (data) {
        $('#users-table-body-js').html(data)
        tableUpdateExecuted = true
    })
}

function getGeneratorConfiguration(page, pageSize) {
    return {
        locale: $('#locale-js').val(),
        seed: $('#seed-js').val(),
        page: page,
        pageSize: pageSize
    }
}

function getRandomInt(max) {
    return Math.floor(Math.random() * max);
}

function switchToTopButton(userScroll) {
    if (userScroll > 100) {
        $('#to-top-js').show()
    } else {
        $('#to-top-js').hide()
    }
}