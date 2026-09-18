type BookFormData = {
    title: string
    author: string
}

type BookFormProps = {
    formData: BookFormData
    isEditing: boolean
    onChange: (field: 'title' | 'author', value: string) => void
    onSubmit: () => void
    onCancel: () => void
}

function BookForm({
    formData,
    isEditing,
    onChange,
    onSubmit,
    onCancel,
}: BookFormProps) {
    return (
        <>
            <input
                value={formData.title}
                onChange={(e) => onChange('title', e.target.value)}
                placeholder="Title"
            />

            <input
                value={formData.author}
                onChange={(e) => onChange('author', e.target.value)}
                placeholder="Author"
            />

            <button onClick={onSubmit}>
                {isEditing ? 'Save' : 'Add book'}
            </button>

            {isEditing && (
                <button onClick={onCancel}>
                    Cancel
                </button>
            )}
        </>
    )
}

export default BookForm