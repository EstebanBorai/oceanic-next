import * as React from 'react';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { faExclamationTriangle, faQuestionCircle } from '@fortawesome/free-solid-svg-icons';

interface IReduxFormInput {
  value: string;
  onChange: any;
}

interface IReduxFormMeta {
  error?: string;
  warning?: string;
  touched: boolean;
}

interface IInputFieldProps {
  label?: string;
  name: string;
  type: string;
  placeholder?: string;
  info?: string;
  input: IReduxFormInput;
  meta: IReduxFormMeta;
};

const InputField = (props: IInputFieldProps) => {
  const [showInfo, setShowInfo] = React.useState(false);

  const toggleInfo = () => setShowInfo(!showInfo);

  return (
    <div className="c-form">
      {
        showInfo ?
        <p className="c-info">
          {props.info}
        </p> :
        null
      }
      {
        props.label ?
        <label htmlFor={props.name}>
          {props.label}
          {
            props.info ?
            <button className="info-btn" onClick={toggleInfo} type="button">
              <FontAwesomeIcon icon={faQuestionCircle} />
            </button> :
            null
          }
        </label> :
        null
      }
      <input
        {...props.input}
        type={props.type}
        name={props.name}
        placeholder={props.placeholder}
        value={props.input.value}
        onChange={props.input.onChange}
      />
      {
        props.meta.touched && props.meta.error ?
        <em className={`help-text${props.meta.error ? ' error' : props.meta.warning ? ' warning' : ''}`}>
          <span dangerouslySetInnerHTML={JSON.parse(props.meta.error)}></span>
          <FontAwesomeIcon icon={faExclamationTriangle} />
        </em> :
        null
      }
    </div>
  );
};

InputField.defaultProps = {
  label: undefined,
  placeholder: '',
  info: undefined
};

export default InputField;
