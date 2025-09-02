import {useEffect, useState} from 'react';
import {getTodos, createTodo, updateTodo, deleteTodo} from './api';
import TodoForm from './TodoForm';
import TodoList from './TodoList';

import './App.css';

function App() {
const [todos, setTodos] = useState([]);
const [page, setPage] = useState(1);
const [hasMore, setHasMore] = useState(true);
const [loading, setLoading] = useState(false);

// Toggles the completion status of a todo.
// Updates both the UI and backend.
async function handleToggle(id) {
  // Look for the todo by id.
  const targetTodo = todos.find(todo => todo.id === id);
  // Create a new todo object with the completion flipped.
  const updatedTodo = { ...targetTodo, isDone: !targetTodo.isDone };

  try {
    // Send the status update to the backend.
    await updateTodo(id, updatedTodo);
    // Only update the UI if the status has changed in the backend.
    // Replace the old todo with the one with the flipped status.
    setTodos(prev =>
      prev.map(todo =>
        todo.id === id ? updatedTodo : todo
      )
    );
  } catch (error) {
    console.error('Failed to update todo status :(', error);
  }
}


// Gets all the todos for the page.
// cusotmPage defines which page to fetch from the database.
// reset if true replacces the current list, else appends to it.
const fetchTodos = async (customPage = page, reset = false) => {
  // Flag for fetching in progress.
  setLoading(true);
  try {
    // Takes the page number and sets the page size to 20 todos.
    const res = await getTodos({ page: customPage, pageSize: 20 });
    // Get the actual data from the response
    const data = res.data;
    // Follows reset behavior as stated above.
    setTodos(prev => reset ? data.todos : [...prev, ...data.todos]);
    // Updates the current page number for future fetches.
    setPage(customPage + 1);
    // Check if there are more pages to load.
    // If yes, keep loading.
    setHasMore(customPage < data.totalPages);
  } catch (error) {
    console.error('Error fetching todos:', error);
  } finally {
    // Stop the fetching flag.
    setLoading(false);
  }
};


// Creates a new todo.
const handleCreate = async (todo) => {
  try {
    await createTodo(todo);
    // After creating a new todo, update the list to also show the new todo.
    fetchTodos(1, true);
  } catch(error){
    console.error('There was a problem creating the todo :( ', error);
  }
};

// runs on load. Fetches todos if there are no todos loaded.
useEffect(() => {
  if (todos.length === 0) {
    fetchTodos(1, true);
  }
}, []);
// Create the page with the todo creation form and a list of todos under it.
// Infinite scrolling will load more entries.
  return (
    <div className='todo-container'>
      <h1>Todo App</h1>
      <TodoForm onCreate={handleCreate}/>
      <TodoList
        todos={todos}
        onToggle={handleToggle}
        onDelete={() => fetchTodos(1, true)}
        infiniteScrollProps={{
          dataLength: todos.length,
          next: fetchTodos,
          hasMore,
          loader: <h4>Loading...</h4>
        }}
/>
    </div>
  );
}



export default App;
