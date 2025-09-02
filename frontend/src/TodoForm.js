import { useState } from 'react';

function TodoForm({ onCreate }) {
  const [title, setTitle] = useState('');
  const [description, setDescription] = useState('');
  const [dueDate, setDueDate] = useState('');

  const handleSubmit = (e) => {
    e.preventDefault();
    if (!title.trim()) return;

    const newTodo = {
      title,
      description,
      dueDate,
      isDone: false,
    };

    onCreate(newTodo);
    setTitle('');
    setDescription('');
    setDueDate('');
  };

  return (
    <form onSubmit={handleSubmit} style={{ marginBottom: '2rem' }}>
      <input
        type="text"
        placeholder="Title"
        value={title}
        onChange={(e) => setTitle(e.target.value)}
        required
        style={{ display: 'block', marginBottom: '0.5rem', width: '100%' }}
      />
      <textarea
        placeholder="Description"
        value={description}
        onChange={(e) => setDescription(e.target.value)}
        style={{ display: 'block', marginBottom: '0.5rem', width: '100%' }}
      />
      <input
        type="date"
        value={dueDate}
        onChange={(e) => setDueDate(e.target.value)}
        style={{ display: 'block', marginBottom: '0.5rem' }}
      />
      <button type="submit">Add Todo</button>
    </form>
  );
}

export default TodoForm;