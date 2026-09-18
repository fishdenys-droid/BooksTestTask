import { useEffect, useState } from 'react'
import BookForm from './components/BookForm'
import BookList from './components/BookList'
import type { Book } from './types'

function App() {
    const [books, setBooks] = useState<Book[]>([])

    // GET — загружаем книги из API
    useEffect(() => {
        console.log('EFFECT')

        fetch('https://localhost:7218/Books/GetAll')
            .then(response => response.json())
            .then(data => {
                console.log('API DATA:', data)
                setBooks(data)
            })
    }, [])

    const [formData, setFormData] = useState({
        title: '',
        author: '',
    })

    const [editingBookId, setEditingBookId] =
        useState<number | null>(null)

    function handleChange(
        field: 'title' | 'author',
        value: string
    ) {
        setFormData({
            ...formData,
            [field]: value,
        })
    }

    function addBook() {
        setBooks([
            ...books,
            {
                id: Date.now(),
                title: formData.title,
                author: formData.author,
                yearPublished: null,
                contents: null,
            },
        ])

        setFormData({
            title: '',
            author: '',
        })
    }

    function editBook(book: Book) {
        setEditingBookId(book.id)

        setFormData({
            title: book.title,
            author: book.author,
        })
    }

    function saveBook() {
        setBooks(
            books.map((book) =>
                book.id === editingBookId
                    ? {
                        ...book,
                        title: formData.title,
                        author: formData.author,
                    }
                    : book
            )
        )

        setEditingBookId(null)

        setFormData({
            title: '',
            author: '',
        })
    }

    function deleteBook(id: number) {
        setBooks(
            books.filter((book) => book.id !== id)
        )
    }

    function cancelEdit() {
        setEditingBookId(null)

        setFormData({
            title: '',
            author: '',
        })
    }

    return (
        <>
            <h1>Books</h1>

            <BookForm
                formData={formData}
                isEditing={editingBookId !== null}
                onChange={handleChange}
                onSubmit={
                    editingBookId !== null
                        ? saveBook
                        : addBook
                }
                onCancel={cancelEdit}
            />

            <BookList
                books={books}
                onDelete={deleteBook}
                onEdit={editBook}
            />
        </>
    )
}

export default App