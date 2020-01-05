import React from 'react';

const THEMES = {
	DARK: 'dark',
	WHITE: 'white'
}

class App extends React.Component {
  render() {
    return <Toolbar theme="dark" />;
  }
}

// Arrow Functions
const Button = ({ theme, text }) => {
	const THEME_COLORS = {
		dark: {
			color: '#fff',
			backgroundColor: '#000'
		},
		white: {
			color: '#000',
			backgroundColor: '#fff'
		}
	}

	// If/Else
	if (theme === THEMES.DARK) {
		return (
			<button
				style={THEME_COLORS[THEMES.DARK]}
			>
				{text}
			</button>
		);
	} else if (theme === THEMES.WHITE) {
		return (
			<button style={THEME_COLORS[THEMES.WHITE]}>
				{text}
			</button>
		);
	}

	return <button>{text}</button>;
}

function Toolbar(props) {
  // The Toolbar component must take an extra "theme" prop
  // and pass it to the ThemedButton. This can become painful
  // if every single button in the app needs to know the theme
  // because it would have to be passed through all components.
  return (
    <div>
      <ThemedButton theme={props.theme} />
    </div>
  );
}

class ThemedButton extends React.Component {
  render() {
    return <Button theme={this.props.theme} />;
  }
}

export default App;
