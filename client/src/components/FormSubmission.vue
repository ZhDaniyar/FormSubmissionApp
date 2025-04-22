<template>
  <div class="form-container">
    <h3>Dynamic Form Submission</h3>
    <form @submit.prevent="handleSubmit">
      <div class="form-group" :class="{ 'error': errors.name }">
        <label for="name">Name:</label>
        <input type="text" id="name" v-model="formData.name" @blur="validateField('name')" />
        <div v-if="errors.name" class="error-message">{{ errors.name }}</div>
      </div>
      
      <div class="form-group" :class="{ 'error': errors.category }">
        <label for="category">Category:</label>
        <select id="category" v-model="formData.category" @change="validateField('category')">
          <option value="">Please select</option>
          <option value="Bug">Bug</option>
          <option value="Feature Request">Feature Request</option>
        </select>
        <div v-if="errors.category" class="error-message">{{ errors.category }}</div>
      </div>
      
      <div class="form-group" :class="{ 'error': errors.date }">
        <label for="date">Date:</label>
        <input type="date" id="date" v-model="formData.date" @change="validateField('date')" />
        <div v-if="errors.date" class="error-message">{{ errors.date }}</div>
      </div>
      
      <div class="form-group priority-group" :class="{ 'error': errors.priority }">
        <label>Priority:</label>
        <div class="priority-options">
          <label><input type="radio" v-model="formData.priority" value="Low" @change="validateField('priority')" /> Low</label>
          <label><input type="radio" v-model="formData.priority" value="Medium" @change="validateField('priority')" /> Medium</label>
          <label><input type="radio" v-model="formData.priority" value="High" @change="validateField('priority')" /> High</label>
        </div>
        <div v-if="errors.priority" class="error-message">{{ errors.priority }}</div>
      </div>
      
      <div class="form-group terms-group" :class="{ 'error': errors.agree }">
        <input type="checkbox" id="terms" v-model="formData.agree" @change="validateField('agree')" />
        <label for="terms">I agree to the terms</label>
        <div v-if="errors.agree" class="error-message">{{ errors.agree }}</div>
      </div>
      <div class="button-container">
        <button type="submit" class="submit-button" :disabled="isSubmitting">
          {{ isSubmitting ? 'Submitting...' : 'Submit' }}
        </button>
      </div>
    </form>
  </div>
</template>

<script>
export default {
  name: 'FormSubmission',
  props: {
    onSubmit: {
      type: Function,
      required: true
    }
  },
  data() {
    return {
      formData: {
        name: '',
        category: '',
        date: '',
        priority: '',
        agree: false
      },
      errors: {
        name: '',
        category: '',
        date: '',
        priority: '',
        agree: ''
      },
      isSubmitting: false
    };
  },
  methods: {
    validateField(field) {
      switch(field) {
        case 'name':
          this.errors.name = !this.formData.name.trim() ? 'Name is required' : '';
          break;
        case 'category':
          this.errors.category = !this.formData.category ? 'Category is required' : '';
          break;
        case 'date':
          this.errors.date = !this.formData.date ? 'Date is required' : '';
          break;
        case 'priority':
          this.errors.priority = !this.formData.priority ? 'Priority is required' : '';
          break;
        case 'agree':
          this.errors.agree = !this.formData.agree ? 'You must agree to the terms' : '';
          break;
      }
    },
    validateForm() {
      // Validate all fields
      this.validateField('name');
      this.validateField('category');
      this.validateField('date');
      this.validateField('priority');
      this.validateField('agree');
      
      // Check if there are any errors
      return !Object.values(this.errors).some(error => error);
    },
    handleSubmit() {
      if (this.validateForm()) {
        this.isSubmitting = true;
        
        try {
          this.onSubmit({...this.formData});
          this.resetForm();
        } catch (error) {
          console.error('Error submitting form:', error);
        } finally {
          this.isSubmitting = false;
        }
      }
    },
    resetForm() {
      this.formData = {
        name: '',
        category: '',
        date: '',
        priority: '',
        agree: false
      };
      
      // Clear all errors
      Object.keys(this.errors).forEach(key => {
        this.errors[key] = '';
      });
    }
  }
};
</script>

<style>
.form-container {
  background: #fff;
  padding: 20px;
  border-radius: 8px;
  box-shadow: 0 2px 4px rgba(0, 0, 0, 0.1);
  width: 80%;
  margin: auto;
}

.form-group {
  display: flex;
  flex-direction: column;
  margin-bottom: 15px;
}

.form-group label {
  font-weight: bold;
  margin-bottom: 5px;
}

.form-group input,
.form-group select {
  padding: 8px;
  border: 1px solid #ccc;
  border-radius: 4px;
}

.priority-group .priority-options {
  display: flex;
  gap: 10px;
}

.terms-group {
  display: flex;
  align-items: center;
  gap: 5px;
}

.button-container {
  display: flex;
  justify-content: center;
  margin-top: 20px;
}

.submit-button {
  background: #007bff;
  color: white;
  border: none;
  padding: 10px 20px;
  border-radius: 4px;
  cursor: pointer;
}

.submit-button:hover {
  background: #0056b3;
}

.submit-button:disabled {
  background: #cccccc;
  cursor: not-allowed;
}

.form-group.error input,
.form-group.error select {
  border-color: #dc3545;
}

.error-message {
  color: #dc3545;
  font-size: 0.8rem;
  margin-top: 5px;
}
</style>