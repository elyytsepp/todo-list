import { useState } from 'react';
import { deleteTodo } from './api';
import { updateTodo } from './api';
import './App.css';
import InfiniteScroll from 'react-infinite-scroll-component';

// todos - todo item array
// onToggle - toggle completion status
// onDelete - refresh the lsit after delete or update
// infinite - props for pagination
function TodoList({ todos, onToggle, onDelete, infiniteScrollProps }) {
    // Used to track which todos details are expanded.
    const [expandedIds, setExpandedIds] = useState([]);
    const toggleExpand = (id) => {
        setExpandedIds((prev) =>
        prev.includes(id) ? prev.filter((x) => x !== id) : [...prev, id]
        );
    };

    // Handles todo deletion.
    const handleDelete = async (id) => {
    try {
      // Sends command to API to delete from database.
        await deleteTodo(id);
        // Sends trigger to App.js to update UI.
        onDelete();        
    } catch (error) {
        console.error('Error deleting todo:', error);
    }
    };

    // Handles saving edited fields of a todo.
    const handleSave = async (id) => {
        try {
            const updatedFields = {
            description: editedDescription,
            dueDate: editedDueDate,
            };
            // Call the API to update database.
            await updateTodo(id, updatedFields);
            // Stop editing.
            setEditingTodoId(null);
            // Update todo list in UI.
            onDelete();
        } catch (error) {
            console.error('Error updating todo:', error);
        }
        };
    // State for editing mode and form fields.
    const [editingTodoId, setEditingTodoId] = useState(null);
    const [editedDescription, setEditedDescription] = useState('');
    const [editedDueDate, setEditedDueDate] = useState('');
  
  // Render the todo list with infinite scrolling.
  return (
    <InfiniteScroll {... infiniteScrollProps}>
      <ul className="todo-list">
      {todos.map((todo, index) => {
        // Check if the current todo is expanded.
        const isExpanded = expandedIds.includes(todo.id);
        return (
          // Change outline in UI based on completion, toggle expansion on click.
            <li key={todo.id} className={`todo-item ${todo.isDone ? 'done' : 'not-done'}`} onClick={() => toggleExpand(todo.id)} style={{ '--i':index }} id={'todo-${todo.id}'}>
          {/*Todo summary always displayed*/}
          <h3 className="todo-title clickable" >
            <div className='todo-left'>
                <span className="todo-id">{todo.id}.</span> {todo.text}
                {todo.title}                
            </div>
            <div className="todo-right">
                <span className="todo-dueDate">{todo.dueDate ? new Date(todo.dueDate).toLocaleDateString() : 'No due date'}</span>
                <span className={`arrow ${isExpanded ? 'expanded' : ''}`}>▸</span>
            </div>
            </h3>
          {/*Todo details displayed when expanded*/}  
          {isExpanded && (
            <div className = 'todo-details'>
                <p className="todo-description">{todo.description}</p>
          <p className="todo-meta">
            <strong>Created:</strong>{' '}
            {todo.createdDate ? new Date(todo.createdDate).toLocaleDateString() : 'No created date'}
          </p>
          <p className="todo-meta">
            <strong>Due:</strong>{' '}
            {todo.dueDate ? new Date(todo.dueDate).toLocaleDateString() : 'No due date'}
          </p>
          {/*Todo completion status toggle*/}
          <label className="todo-status" onClick={(e) => e.stopPropagation()}>
            <strong>Status: </strong>
            <input
              type="checkbox"
              className="checkbox"
              checked={todo.isDone}
              onChange={() => onToggle(todo.id)}
            />
            {todo.isDone ? 'Done' : 'Not done'}
          </label>
          {/*Edit and Delete buttons lined up with the ID*/}
          <div className='id-delete'>
            <p className="todo-id"><strong>ID:</strong> {todo.id}</p>
            <div className='edit-delete'>
                <button className='edit-btn' onClick={(e) => {
                e.stopPropagation();
                setEditingTodoId(todo.id);
                setEditedDescription(todo.description);
                setEditedDueDate(todo.dueDate);
            }}
            >Edit</button>
                <button className='delete-btn' onClick={(e) => {
                e.stopPropagation();
                handleDelete(todo.id);
            }}
            >Delete</button>
            </div>
            {/*Modal for editing todo fields after clicking the edit button.*/}
            {editingTodoId === todo.id && (
                <div className="modal" onClick={(e) => {e.stopPropagation()}}>
                    <h2>{todo.title}</h2>
                    <textarea
                    value={editedDescription}
                    onChange={(e) => setEditedDescription(e.target.value)}
                    />
                    <input
                    type="date"
                    value={editedDueDate}
                    onChange={(e) => setEditedDueDate(e.target.value)}
                    />
                    <button onClick={() => handleSave(todo.id)}>Save</button>
                    <button onClick={() => setEditingTodoId(null)}>Cancel</button>
                </div>
            )}
            
          </div>
            </div>
          )}
        </li>
        );
        })}
    </ul>
    </InfiniteScroll>
  );
}

export default TodoList;