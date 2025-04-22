<template>
  <div class="container">
    <h1>Dynamic Form Submission</h1>

    <div v-if="notification" class="notification" :class="notification.type">
      {{ notification.message }}
    </div>

    <FormSubmission :onSubmit="submitForm" />
    <SubmissionList :submissions="submissions" />
  </div>
</template>

<script>
import FormSubmission from './components/FormSubmission.vue';
import SubmissionList from './components/SubmissionList.vue';

export default {
  components: { FormSubmission, SubmissionList },
  data() {
    return { 
      submissions: [],
      notification: null
    };
  },
  methods: {
    showNotification(message, type = 'success') {
      this.notification = { message, type };
      setTimeout(() => {
        this.notification = null;
      }, 3000);
    },
    async fetchSubmissions() {
      try {
        const res = await fetch('http://localhost:5003/api/submissions');
        if (!res.ok) {
          throw new Error('Failed to fetch submissions');
        }
        this.submissions = await res.json();
      } catch (error) {
        console.error('Error fetching submissions:', error);
        this.showNotification('Failed to load submissions', 'error');
      }
    },
    async submitForm(formData) {
      try {
        const res = await fetch('http://localhost:5003/api/submissions', {
          method: 'POST',
          headers: { 'Content-Type': 'application/json' },
          body: JSON.stringify(formData)
        });
        
        if (!res.ok) {
          throw new Error('Failed to submit form');
        }
        
        const result = await res.json();
        this.showNotification('Form submitted successfully!');
        await this.fetchSubmissions();
      } catch (error) {
        console.error('Error submitting form:', error);
        this.showNotification('Failed to submit form', 'error');
      }
    }
  },
  mounted() {
    this.fetchSubmissions();
  }
};
</script>

<style>
.container {
  max-width: 600px;
  margin: 2rem auto;
  padding: 1rem;
  border: 1px solid #ccc;
  border-radius: 10px;
}

.notification {
  padding: 10px;
  margin-bottom: 15px;
  border-radius: 4px;
  text-align: center;
}

.notification.success {
  background-color: #d4edda;
  color: #155724;
  border: 1px solid #c3e6cb;
}

.notification.error {
  background-color: #f8d7da;
  color: #721c24;
  border: 1px solid #f5c6cb;
}
</style>