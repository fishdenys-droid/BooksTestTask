import type { Book } from '../types'

type BookItemProps = {
    book: Book
    onDelete: (id: number) => void
    onEdit: (book: Book) => void
}

function BookItem({ book, onDelete, onEdit }: BookItemProps) {
    return (
        <div>
            <h3>{book.title}</h3>

            <p>Автор: {book.author}</p>

            <p>
                Год публикации: {book.yearPublished ?? 'Не указан'}
            </p>

            <p>
                {book.contents ?? 'Нет описания'}
            </p>

            <button onClick={() => onEdit(book)}>
                Редактировать
            </button>

            <button onClick={() => onDelete(book.id)}>
                Удалить
            </button>
        </div>
    )
}

export default BookItem