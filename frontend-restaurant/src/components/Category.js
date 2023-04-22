import React, { Fragment, useState } from 'react'
import axios from 'axios'  
import './Category.css'
import FormInput from "./FormInput";
import ReactDOM from 'react-dom';

const RadioButton = ({ name, id, value, onChange, checked, text }) => {
  return (
    <label htmlFor={id} className="radio-label">
      <input
        className="radio-input"
        type="radio"
        name={name}
        id={id}
        value={value}
        onChange={onChange}
        checked={checked}
      />
      <span className="custom-radio" />
      {text}
    </label>
  )
}

const App = () => {
  const [values, setValues] = useState({
    username: "",
  });

  const [theme, setTheme] = React.useState({ dark: false, light: false })

  const onChangeTheme = (e) => {
    const { name } = e.target
    if (name === 'light') {
      setTheme({ dark: false, light: true })
    }
    if (name === 'dark') {
      setTheme({ dark: true, light: false })
    }
  }

  const [theme1, setTheme1] = React.useState({ yes: false, no: false })

  const onChangeTheme1 = (e) => {
    const { name } = e.target
    if (name === 'no') {
      setTheme1({ yes: false, no: true })
    }
    if (name === 'yes') {
      setTheme1({ yes: true, no: false })
    }
  }

  const inputs = [
    {
      id: 1,
      name: "Title",
      type: "text",
      placeholder: "Title",
      errorMessage:
        "Title should be 3-16 characters and shouldn't include any special character!",
      label: "Title",
      pattern: "^[A-Za-z0-9]{3,16}$",
      required: true,
    },
  ];


  const handleSubmit = (e) => {
    e.preventDefault();
  };

  const onChange = (e) => {
    setValues({ ...values, [e.target.name]: e.target.value });
  };

  return (
    <div className="app">
      <form onSubmit={handleSubmit}>
        <h1>Category</h1>
        {inputs.map((input) => (
          <FormInput
            key={input.id}
            {...input}
            value={values[input.name]}
            onChange={onChange}
          />
        ))}
          <div className="select-theme">
          <label>Featured</label><br/>
          <RadioButton
        name="dark"
        id="dark"
        value="Dark"
        text="Yes"
        onChange={onChangeTheme}
        checked={theme.dark}
      />
      <RadioButton
        name="light"
        id="light"
        value="Light"
        text="No"
        onChange={onChangeTheme}
        checked={theme.light}
      />
          </div>
          <div className="select-theme">
          <label>Active</label><br/>
          <RadioButton
        name="yes"
        id="yes"
        value="Yes"
        text="Yes"
        onChange={onChangeTheme1}
        checked={theme1.yes}
      />
      <RadioButton
        name="no"
        id="no"
        value="No"
        text="No"
        onChange={onChangeTheme1}
        checked={theme1.no}
      />
          </div>
        <button className='button'>Submit</button>
      </form>
    </div>
  );
};

ReactDOM.render(<App />, document.getElementById('root'));
export default App;