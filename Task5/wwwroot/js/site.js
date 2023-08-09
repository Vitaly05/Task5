const MAX_SEED_VALUE = 2_147_483_646

$(document).ready(async function() {
    await updateTableData()

    $('.generator-configuration-js').on('change', async function() {
        validateFields()
        await updateTableData()
    })

    $('#get-csv-js').click(getCsvFile)
    
    $('#random-seed-js').click(async e => {
        e.preventDefault()
        $('#seed-js').val(getRandomInt(MAX_SEED_VALUE))
        await updateTableData()
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

var currentPage = 0

$(window).scroll(async () => {
    const userScroll = $(window).scrollTop()
    switchToTopButton(userScroll)
    if (userScroll + $(window).height() >= $(document).height() - 1 && tableUpdateExecuted) {
        tableUpdateExecuted = false
        appendTableData()
    }
})

async function updateTableData() {
    await resetTableData()
    await appendTableData()
}

async function appendTableData() {
    await $.post('/getUsersData', getGeneratorConfiguration(currentPage + 1), function (data) { 
        $('#users-table-body-js').append(data)
        currentPage++
        tableUpdateExecuted = true
    })
}

async function resetTableData() {
    currentPage = 0
    await $.post('/getUsersData', getGeneratorConfiguration(), function (data) {
        $('#users-table-body-js').html(data)
        tableUpdateExecuted = true
    })
}

function getCsvFile() {
    $.post('/getCsvFile', getGeneratorConfiguration(currentPage), function(data) {
        var blob = new Blob([data])
        var link = document.createElement('a')
        link.href = window.URL.createObjectURL(blob)
        link.download = "FakeData.csv"
        link.click()
    })
}

function getGeneratorConfiguration(page) {
    return {
        locale: $('#locale-js').val(),
        seed: $('#seed-js').val(),
        page: page,
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