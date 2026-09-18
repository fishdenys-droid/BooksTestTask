import BookItem from './BookItem'
import type { Book } from '../types'

type BookListProps = {
    books: Book[]
    onDelete: (id: number) => void
    onEdit: (book: Book) => void
}

function BookList({ books, onDelete, onEdit }: BookListProps) {
    return (
        <div>
            {books.map((book) => (
                <BookItem
                    key={book.id}
                    book={book}
                    onDelete={onDelete}
                    onEdit={onEdit}
                />
            ))}
        </div>
    )
}

export default BookList