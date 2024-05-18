// ++++++++++++++++++++++++++++++++++++++++++++++++++

document.addEventListener('DOMContentLoaded', function() {
    fetchCategories();
    fetchBooks();
});

function fetchCategories() {
    const requestOptions = {
        method: 'GET',
        headers: {
            'Content-Type': 'application/json'
        }
    };
     fetch('http://localhost:7087/api/Categories', requestOptions)
        .then(response => response.json())
        .then(data => {
            //console.log(data);
            const dropdown = document.getElementById('categoryDropdown');
            data.forEach(category => {
                const option = document.createElement('option');
                option.value = category.name;
                option.textContent = category.name;
                dropdown.appendChild(option);
            });
        })
        .catch(error => console.error('Error fetching categories:', error));
}

async function fetchBooks() {
    const requestOptions = {
        method: 'GET',
        headers: {
            'Content-Type': 'application/json'
        }
    };
    await fetch('http://localhost:7087/api/Books', requestOptions)
        .then(response => response.json())
        .then(data => displayBooks(data))
        .catch(error => console.error('Error fetching books:', error));
}

async function filterBooksByCategory() {
    const selectedCategory = document.getElementById('categoryDropdown').value;
    if (selectedCategory === 'all') {
        await fetchBooks();
    } else {
        await fetch(`http://localhost:7087/api/Books/category/${selectedCategory}`)
            .then(response => response.json())
            .then(data => displayBooks(data))
            .catch(error => console.error('Error fetching books by category:', error));
    }
}

async function displayBooks(books) {
    const container = document.getElementById('booksContainer');
    container.innerHTML = '';
    books.forEach(book => {
        const card = document.createElement('div');
        card.className = 'book-card';
        card.innerHTML = `
            <img src="${book.coverImagePath}" alt="${book.title}" class="book-cover">
            <h3 class="book-title">${book.title}</h3>
            <p class="book-author">Author: ${book.author.name}</p>
            <p class="book-category">Category: ${book.category.name}</p>
        `;
        card.addEventListener('click', () => showBookDetails(book));
        container.appendChild(card);
    });
}

async function showBookDetails(book) {
    document.getElementById('modalBookTitle').textContent = book.title;
    document.getElementById('modalBookAuthor').textContent = `Author: ${book.author.name}`;
    document.getElementById('modalBookCategory').textContent = `Category: ${book.category.name}`;
    document.getElementById('modalBookDescription').textContent = book.description;
    document.getElementById('bookDetailsModal').style.display = 'block';
}

function closeModal() {
    document.getElementById('bookDetailsModal').style.display = 'none';
}
