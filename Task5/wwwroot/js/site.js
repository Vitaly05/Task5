const MAX_SEED_VALUE = 2_147_483_646

$(document).ready(function() {
    $('.generator-configuration-js').on('input', function() {
        validateFields()
        updateTableData()
    })
    
    $('#random-seed-js').click(e => {
        e.preventDefault()
        $('#seed-js').val(getRandomInt(MAX_SEED_VALUE))
        updateTableData()
    })
    
    $('#mistakes-count-range-js').on('input', function() {
        $('#mistakes-count-input-js').val($(this).val())
    })
    
    $('#mistakes-count-input-js').on('input', function() {
        $('#mistakes-count-range-js').val($(this).val())
    })
})

function validateFields() {
    $('#mistakes-count-input-js').val(getValidValue($('#mistakes-count-input-js').val(), 0, 1000, true))
    $('#seed-js').val(getValidValue($('#seed-js').val(), 0, MAX_SEED_VALUE, false))
}

function getValidValue(input, min, max, allowDot) {
    let value = allowDot ? input.replace(/[^\d\.-]/g, '') : input.replace(/[^\d-]/g, '')
    if (value < min || value > max)
        value = Math.min(Math.max(value, min), max)
    if (isNaN(value)) value = 0
    return value
}

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
        pageSize: pageSize,
        mistakesCount: $('#mistakes-count-input-js').val()
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