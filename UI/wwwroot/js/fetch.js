

const uri = 'https://localhost:7214/items';


document.getElementById('btnFetch').addEventListener('click', () => {
    fetch(uri)
        .then(resp => {
            if (!resp.ok)
                throw new Error(`HTTP error: ${resp.status}`);
            return resp.json();
        })
        .then(data => {
            let markup = '<ul>';
            data.forEach(item => {
                markup += `<li>${item.name} ${item.price}</li>`;
            });
            markup += '</ul>';
            document.getElementById('divFetch').innerHTML = markup;
        })
        .catch(err => {
            document.getElementById('divFetch').innerHTML = err;
        });
});



