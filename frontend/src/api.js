import axios from 'axios';

const BASE_URL = 'http://localhost:8080/api/todo';

export const getTodos = (params) => axios.get(BASE_URL, { params });
export const createTodo = (todo) => axios.post(BASE_URL, todo);
export const updateTodo = (id, todo) => axios.put(`${BASE_URL}/${id}`, todo);
export const deleteTodo = (id) => axios.delete(`${BASE_URL}/${id}`);