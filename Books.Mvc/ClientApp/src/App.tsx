import { useEffect, useState } from 'react'
import BookForm from './components/BookForm'
import BookList from './components/BookList'
import type { Book } from './types'

function App() {
    const [books, setBooks] = useState<Book[]>([])

    // GET — загружаем книги из API
    useEffect(() => {
        console.log('EFFECT')

        fetch('https://localhost:7218/api/books')
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

    const addBook = async () => {
        const response = await fetch('https://localhost:7218/api/books', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify({
                title: formData.title,
                author: formData.author
            })
        })

        const createdBook = await response.json()

        setBooks([...books, createdBook])
        
    }

    const deleteBook = async (id: number) => {
        const response = await fetch(`https://localhost:7218/api/books/${id}`, {
            method: 'DELETE'
        })

        if (!response.ok) {
            console.error('Ошибка удаления')
            return
        }

        setBooks(books.filter(book => book.id !== id))
    }

    const saveBook = async () => {
        if (editingBookId === null) {
            return
        }

        const currentBook = books.find(
            book => book.id === editingBookId
        )

        if (!currentBook) {
            return
        }

        const updatedBook = {
            ...currentBook,
            title: formData.title,
            author: formData.author,
        }

        const response = await fetch(
            `https://localhost:7218/api/books/${editingBookId}`,
            {
                method: 'PUT',
                headers: {
                    'Content-Type': 'application/json',
                },
                body: JSON.stringify(updatedBook),
            }
        )

        if (!response.ok) {
            console.error('Ошибка обновления')
            return
        }

        const savedBook = await response.json()

        setBooks(prevBooks =>
            prevBooks.map(book =>
                book.id === savedBook.id
                    ? savedBook
                    : book
            )
        )

        setEditingBookId(null)

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